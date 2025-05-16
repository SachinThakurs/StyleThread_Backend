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
    public record ConfirmEmailCommand(string UserId, string Token) : IRequest<GenericResponse<string>>;

    internal class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, GenericResponse<string>>
    {
        private readonly UserManager<Customer> _userManager;

        public ConfirmEmailHandler(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GenericResponse<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            return result.Succeeded
                ? new GenericResponse<string> { Success = true, Message = "Email confirmed." }
                : new GenericResponse<string> { Success = false, Message = "Invalid confirmation." };
        }
    }
}
