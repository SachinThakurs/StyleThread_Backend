using Application.DTO;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Application.DTO.Auth;

namespace Persistence.Repository // Corrected spelling from 'Presistance' to 'Persistence'
{
    public class UserService(UserManager<Customer> _userManager, SignInManager<Customer> _signInManager) : IUserService 
    {

        public async Task<IdentityResult> Register(Customer registerRequest, string password)
        {
            IdentityResult? result = await _userManager.CreateAsync(registerRequest, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerRequest, "ADMINISTRATOR");
            }

            return result;
        }

        public async Task<GenericResponse<string>> Login(LoginRequestDto loginRequest)
        {
            Customer? user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                SignInResult varifyUser = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
                if (varifyUser.Succeeded)
                {
                    return new GenericResponse<string> { Content = await GenerateJwtTokenAsync(user), Success = true, Message = "Login successful." };
                }
                return new GenericResponse<string> { Content = "Error", Success = false, Message = "Please Enter the Valid Password" };
            }
            return new GenericResponse<string> { Content = "Error", Success = false, Message = "Please Enter the Valid Credentials" };
        }
        private async Task<string> GenerateJwtTokenAsync(Customer user)
        {

            List<Claim>? claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            IList<string> roles = await _userManager.GetRolesAsync(user);

            if (roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim("role", role)));
            }
            SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THISISMYJSONWEBTOKENSOITSMYCHOICE"));
            SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken? token = new JwtSecurityToken(
                issuer: "SACHIN",
                audience: null, // Customize this if you have an audience
                claims: claims,
                expires: DateTime.Now.AddHours(24), // Token expiration
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<string> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded ? "Account deleted successfully." : "Error while deleting the account.";
            }
            return "User not found.";
        }

        public async Task<string> UpdateUser(Customer updateUserRequest)
        {
            var user = await _userManager.FindByIdAsync(updateUserRequest.Id);
            if (user != null)
            {
                user.FirstName = updateUserRequest.FirstName;
                user.LastName = updateUserRequest.LastName;
                user.Email = updateUserRequest.Email;

                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded ? "Account information updated successfully." : "Error while updating account data.";
            }
            return "User not found.";
        }
    }
}
