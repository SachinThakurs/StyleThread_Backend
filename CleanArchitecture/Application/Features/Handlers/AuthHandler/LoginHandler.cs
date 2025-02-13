using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public class LoginHandler(IUserService _userService) : IRequestHandler<GenericCreateCommand<LoginRequestDto, GenericResponse<CustomerDto>>, GenericResponse<CustomerDto>>
    {
        public async Task<GenericResponse<CustomerDto>> Handle(GenericCreateCommand<LoginRequestDto, GenericResponse<CustomerDto>> request, CancellationToken cancellationToken)
        {
            if (request?.Entity == null)
                return new GenericResponse<CustomerDto> { Success = false, Message = "Login request cannot be null." };
            try
            {
                return (await _userService.Login(request.Entity));
            }
            catch (Exception ex)
            {
                return new GenericResponse<CustomerDto> { Success = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
