using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.CartHandler
{
    internal class UpdateCartHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GenericUpdateCommand<CartDto, GenericResponse<string>>, GenericResponse<string>>
    {
        public async Task<GenericResponse<string>> Handle(GenericUpdateCommand<CartDto, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    Cart findCartItem = await _unitOfWork.cartRepository.GetByIdAsync(request.Entity.CartId, cancellationToken);
                    if (findCartItem != null)
                    {
                        var updateCartBinding = _mapper.Map(request.Entity, findCartItem);
                        await _unitOfWork.cartRepository.UpdateAsync(updateCartBinding);
                        await _unitOfWork.cartRepository.SaveAsync(cancellationToken);
                        return new GenericResponse<string> { Content = "Updated Successfully", Error = "null", Message = "Cart Update Successfully", Success = true };
                    }
                    return new GenericResponse<string> { Content = $"No Record found", Error = "Error while finding the record", Message = $"No Record found with the given Brand Id {request.Entity.CartId}", Success = false };
                }
                return new GenericResponse<string> { Content = null, Error = "Enter valid data", Message = "Please enter the valid data", Success = false };
            }
            catch (Exception ex)
            {
                return new GenericResponse<string>
                {
                    Content = null,
                    Error = ex?.InnerException?.Message,
                    Message = ex?.Message,
                    Success = false
                };
            }
        }
    }
}
