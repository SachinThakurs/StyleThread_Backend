using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class UpdateCartCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GenericUpdateCommand<CartDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<CartDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
            {
                return CreateErrorResponse("Please enter valid data", "Enter valid data");
            }

            var cartItem = await _unitOfWork.cartRepository.GetByIdAsync(request.Entity.CartId, cancellationToken);
            if (cartItem == null)
            {
                return CreateErrorResponse($"No record found with the given Cart Id {request.Entity.CartId}", "Error while finding the record");
            }

            _mapper.Map(request.Entity, cartItem);
            await _unitOfWork.cartRepository.UpdateAsync(cartItem);
            await _unitOfWork.cartRepository.SaveAsync(cancellationToken);

            return new GenericResponse<string>
            {
                Content = "Updated Successfully",
                Message = "Cart updated successfully",
                Success = true
            };
        }
        private GenericResponse<string> CreateErrorResponse(string message, string error)
        {
            return new GenericResponse<string>
            {
                Content = null,
                Error = error,
                Message = message,
                Success = false
            };
        }
    }
}
