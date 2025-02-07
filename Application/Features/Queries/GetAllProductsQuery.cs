using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                // Fetch all products along with their variants and sizes in a single optimized query
                var productData = await _unitOfWork.productRepository.GetAllWithVariantsAndSizesAsync(cancellationToken);

                // Ensure data is mapped properly
                var productDtos = _mapper.Map<IEnumerable<ProductDto>>(productData) ?? Enumerable.Empty<ProductDto>();

                return productDtos;
            }
        }
    }
}
