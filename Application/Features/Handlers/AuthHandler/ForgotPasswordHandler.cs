using Application.Interfaces.IRepository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    public record ForgotPasswordCommand(string Email) : IRequest<GenericResponse<string>>;

    internal class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;

        public ForgotPasswordHandler(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GenericResponse<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Send this token via email (skip email service code here)
            return new GenericResponse<string> { Success = true, Message = "Reset token generated." };
        }
    }
}
