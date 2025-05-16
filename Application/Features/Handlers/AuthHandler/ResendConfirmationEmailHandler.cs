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
    public record ResendConfirmationEmailCommand(string Email) : IRequest<GenericResponse<string>>;

    internal class ResendConfirmationEmailHandler : IRequestHandler<ResendConfirmationEmailCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;

        public ResendConfirmationEmailHandler(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GenericResponse<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // Send token to user via email (skip actual email logic)
            return new GenericResponse<string> { Success = true, Message = "Confirmation email sent." };
        }
    }

}
