using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class CreateCartCommandHandler(IUnitOfWork _unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<CartDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericCreateCommand<CartDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                var cartData = mapper.Map<Cart>(request.Entity);

                await _unitOfWork.cartRepository.InsertAsync(cartData, cancellationToken);
                await _unitOfWork.cartRepository.SaveAsync(cancellationToken);

                return new GenericResponse<string>
                {
                    Content = "Item Successfully added to cart.",
                    Message = "Data added successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
                {
                    Content = "Error occured while add the item in the cart.",
                    Message = "Error occured while saving the data.",
                    Success = false
                };
            }
            
        }
    }
}
