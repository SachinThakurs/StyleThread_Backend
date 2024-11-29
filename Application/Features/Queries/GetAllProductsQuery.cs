using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq; // Ensure this namespace is included for Any() extension method
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        internal class GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
        {
            private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                // Fetch all products
                var productData = await _unitOfWork.productRepository.GetAllAsync(cancellationToken);

                // Map products to DTOs
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productData) ?? Enumerable.Empty<ProductDto>();

                // Check if product DTOs are not empty
                if (productDtos.Any())
                {
                    foreach (var productDto in productDtos)
                    {
                        // Fetch variants for each product
                        var variantData = await _unitOfWork.productVariantRepository.GetByProductIdAsync(productDto.ProductId, cancellationToken);

                        // Map product variants to DTOs
                        productDto.ProductVariants = _mapper.Map<ICollection<ProductVariantDto>>(variantData) ?? new List<ProductVariantDto>();
                        if (productDto.ProductVariants.Any())
                        {
                            foreach (var variant in productDto.ProductVariants)
                            {
                                var variantSizeData = await _unitOfWork.productVariantSize.GetByProductVariantSizeIdAsync(variant.ProductVariantId, cancellationToken);
                                variant.ProductVariantSizes = _mapper.Map<ICollection<ProductVariantSizeDto>>(variantSizeData) ?? new List<ProductVariantSizeDto>();
                            }
                        }
                    }
                }

                return productDtos; // Return the mapped product DTOs
            }
            private string ConvertImageToBase64(string imagePath)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/')); // Adjust if needed
                if (File.Exists(filePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    return Convert.ToBase64String(imageBytes);
                }
                return null; // Or handle the case where the file doesn't exist
            }
        }
    }
}
