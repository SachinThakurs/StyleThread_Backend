using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.AuthHandler
{
    internal class RegisterHandler(IUserService _userManager, IMapper _mapper) : IRequestHandler<GenericCreateCommand<CustomerDto, IdentityResult>, IdentityResult>
    {
        public async Task<IdentityResult> Handle(GenericCreateCommand<CustomerDto, IdentityResult> request, CancellationToken cancellationToken)
        {
            Customer registerRequest = _mapper.Map<Customer>(request.Entity);
            IdentityResult result = await _userManager.Register(registerRequest, request.Entity.Password);
            return result;
        }
    }
}
