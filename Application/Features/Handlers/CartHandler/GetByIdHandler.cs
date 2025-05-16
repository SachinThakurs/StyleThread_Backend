using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class GetByIdHandler(IUnitOfWork _unitOfWork)
        : IRequestHandler<GenericFindByIdCommand<string, GenericResponse<CartDto>>, GenericResponse<CartDto>>
    {
        public async Task<GenericResponse<CartDto>> Handle(GenericFindByIdCommand<string, GenericResponse<CartDto>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    var cart = await _unitOfWork.cartRepository.GetByIdAsync(request.Entity, cancellationToken);
                    if (cart != null)
                    {
                        var dto = new CartDto
                        {
                            CartId = cart.CartId,
                            CustomerId = cart.CustomerId.ToString(),
                            ProductId = cart.ProductId,
                            Quantity = cart.Quantity,
                            // Map other properties as needed
                        };

                        return new GenericResponse<CartDto>
                        {
                            Content = dto,
                            Error = null,
                            Message = "Cart item retrieved successfully.",
                            Success = true
                        };
                    }

                    return new GenericResponse<CartDto>
                    {
                        Content = null,
                        Error = "Not Found",
                        Message = $"No cart found with ID {request.Entity}",
                        Success = false
                    };
                }

                return new GenericResponse<CartDto>
                {
                    Content = null,
                    Error = "Invalid Data",
                    Message = "Provided ID is null.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<CartDto>
                {
                    Content = null,
                    Error = ex.InnerException?.Message ?? ex.Message,
                    Message = "An error occurred while retrieving the cart.",
                    Success = false
                };
            }
        }
    }
}
