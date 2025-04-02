using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class DeleteCartCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GenericDeleteCommand<CartDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericDeleteCommand<CartDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                var findCartItem = _unitOfWork.cartRepository.GetByIdAsync(request.Entity.CartId, cancellationToken);
                if(findCartItem != null)
                {
                    await _unitOfWork.cartRepository.DeleteAsync(findCartItem, cancellationToken);
                    await _unitOfWork.cartRepository.SaveAsync(cancellationToken);
                    return new GenericResponse<string>
                    {
                        Content = "Item removed successfully.",
                        Error = null,
                        Message = "Category delelted Successfully.",
                        Success = true
                    };
                }
                return new GenericResponse<string>
                {
                    Content = "Item not found.",
                    Error = null,
                    Message = "Item not found.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
                {
                    Content = "Error occured while deleting the item.",
                    Error = null,
                    Message = ex.Message ?? ex.InnerException?.Message,
                    Success = false
                };
            }
        }
    }
}
