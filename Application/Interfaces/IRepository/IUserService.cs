using Application.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Application.Interfaces.IRepository
{
    public interface IUserService
    {
        Task<IdentityResult> Register(Customer registerRequest, string password);
        Task<GenericResponse<string>> Login(LoginRequestDto loginRequest);
        Task<string> DeleteUser(string userId);
        Task<string> UpdateUser(Customer updateUserRequest);
    }
}
