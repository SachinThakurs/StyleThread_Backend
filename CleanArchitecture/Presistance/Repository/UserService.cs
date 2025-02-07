using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
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
    public class UserService(UserManager<Customer> _userManager, SignInManager<Customer> _signInManager, IMapper mapper) : IUserService 
    {
        public async Task<GenericResponse<string>> Register(Customer registerRequest, string password)
        {
            // Check if the registerRequest or password is null
            if (registerRequest == null)
                throw new ArgumentNullException(nameof(registerRequest), "Register request cannot be null.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            try
            {
                // Attempt to create the user
                IdentityResult result = await _userManager.CreateAsync(registerRequest, password);
                if (result.Succeeded)
                {
                    // Assign the user to the "Administrator" role
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(registerRequest, "Administrator");
                    if (!roleResult.Succeeded)
                        // Handle role assignment failure
                        return new GenericResponse<string> { Content = null, Success = false, Message = "Error occured while register the user." };
                }
                else
                {
                    // If creation failed, return the errors from the result
                    return new GenericResponse<string> { Content = null, Success = false, Message = result.Errors.FirstOrDefault()?.Description };
                }
                return new GenericResponse<string> { Content = null, Success = true, Message = "User Register successfully." };
            }
            catch (Exception ex)
            {
                // Log the exception (use a logger if available)
                Console.Error.WriteLine($"An error occurred during registration: {ex.Message}");

                // Return a failed IdentityResult with an error
                return new GenericResponse<string> { Content = "Error", Success = false, Message = "Please Enter the Valid Password" };
            }
        }


        public async Task<GenericResponse<CustomerDto>> Login(LoginRequestDto loginRequest)
        {
            Customer? user = await _userManager.FindByEmailAsync(loginRequest.Email);

            CustomerDto customer = mapper.Map<CustomerDto>(user);

            if (user != null)
            {
                SignInResult varifyUser = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
                if (varifyUser.Succeeded)
                {
                    return new GenericResponse<CustomerDto> { Content = customer, Success = true, Message = await GenerateJwtTokenAsync(user) };
                }
                return new GenericResponse<CustomerDto> { Content = null, Success = false, Message = "Please Enter the Valid Password" };
            }
            return new GenericResponse<CustomerDto> { Content = null, Success = false, Message = "Please Enter the Valid Credentials" };
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
