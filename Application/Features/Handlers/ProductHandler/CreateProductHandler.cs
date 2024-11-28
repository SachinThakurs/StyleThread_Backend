using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Service;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Features.Handlers.ProductHandler
{
    internal class CreateProductHandler : IRequestHandler<GenericCreateCommand<ProductDto, GenericResponse<ProductDto>>, GenericResponse<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CloudinaryService _cloudinaryService;

        public CreateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, CloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cloudinaryService = cloudinaryService;
        }

        public async Task<GenericResponse<ProductDto>> Handle(GenericCreateCommand<ProductDto, GenericResponse<ProductDto>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity == null) throw new ArgumentNullException(nameof(request.Entity), "Product data cannot be null.");

                // Map the ProductDto to Product entity
                Product product = _mapper.Map<Product>(request.Entity);

                // Process images for each product variant
                foreach (var variant in product.ProductVariants)
                {
                    if (variant.Image != null && variant.Image.Count > 0)
                    {
                        var uploadedImageUrls = new List<string>();

                        foreach (var image in variant.Image) // Assuming Image is List<byte[]>
                        {
                            string fileName = $"{Guid.NewGuid()}.jpg"; // Generate a unique file name
                            byte[] imageByteArray = Convert.FromBase64String(image);
                            var imageUrl = await _cloudinaryService.UploadImageAsync(imageByteArray, fileName);
                            if(imageUrl == null)
                            {
                                throw new InvalidOperationException("Error while processing the image.");
                            }
                            uploadedImageUrls.Add(imageUrl);
                        }

                        // Update the Image property with Cloudinary URLs
                        variant.Image = uploadedImageUrls; // Update your model to store image URLs instead of byte[]
                    }
                }

                // Validate Product if needed
                if (product == null)
                    throw new InvalidOperationException("Mapping to Product entity failed.");

                // Save Product
                await _unitOfWork.productRepository.InsertAsync(product);
                await _unitOfWork.productRepository.SaveAsync(cancellationToken);

                var responseData = _mapper.Map<ProductDto>(product);

                return new GenericResponse<ProductDto>
                {
                    Content = responseData,
                    Message = "Product and variants created successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return CreateErrorResponse("An error occurred while processing your request.", ex.Message);
            }
        }


        private async Task<List<string>> ProcessImagesAsync(List<string> base64Images, CancellationToken cancellationToken)
        {
            var imagePaths = new List<string>();
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            foreach (var base64Image in base64Images)
            {
                if (string.IsNullOrWhiteSpace(base64Image) || !base64Image.Contains(","))
                {
                    throw new ArgumentException("Invalid base64 image format.", nameof(base64Image));
                }

                var parts = base64Image.Split(',');
                if (parts.Length < 2)
                {
                    throw new ArgumentException("Base64 image string does not contain valid data.", nameof(base64Image));
                }

                var imageData = parts[1]; // Get the actual base64 data
                var imageBytes = Convert.FromBase64String(imageData);

                var fileName = Guid.NewGuid().ToString() + ".png"; // Unique file name
                var filePath = Path.Combine(uploadDirectory, fileName);

                await File.WriteAllBytesAsync(filePath, imageBytes, cancellationToken); // Save the image

                imagePaths.Add($"/images/{fileName}"); // Store relative path
            }

            return imagePaths;
        }


        private GenericResponse<ProductDto> CreateErrorResponse(string message, string error)
        {
            return new GenericResponse<ProductDto>
            {
                Content = null,
                Error = error,
                Message = message,
                Success = false
            };
        }
    }
}
