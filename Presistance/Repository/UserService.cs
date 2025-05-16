﻿using Application.DTO;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Application.DTO.Auth;

namespace Persistence.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public UserService(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(Customer registerRequest, string password)
        {
            var result = await _userManager.CreateAsync(registerRequest, password);

            if (result.Succeeded)
            {
                // Assign VISITOR role
                await _userManager.AddToRoleAsync(registerRequest, "ADMINISTRATOR");

                SendConfirmationEmailAsync(registerRequest, "http://localhost:5176/");
                // Send confirmation email logic can be triggered here or externally
            }

            return result;
        }

        public async Task<GenericResponse<string>> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        return new GenericResponse<string>
                        {
                            Content = "Error",
                            Success = false,
                            Message = "Please confirm your email first."
                        };
                    }

                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
                    if (result.Succeeded)
                    {
                        return new GenericResponse<string>
                        {
                            Content = await GenerateJwtTokenAsync(user),
                            Success = true,
                            Message = "Login successful."
                        };
                    }

                    return new GenericResponse<string>
                    {
                        Content = "Error",
                        Success = false,
                        Message = "Please enter a valid password."
                    };
                }

                return new GenericResponse<string>
                {
                    Content = "Error",
                    Success = false,
                    Message = "Please enter valid credentials."
                };
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private async Task<string> GenerateJwtTokenAsync(Customer user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THISISMYJSONWEBTOKENSOITSMYCHOICE"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "SACHIN",
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
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
            var user = await _userManager.FindByIdAsync(updateUserRequest.Id.ToString());
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

        public async Task<GenericResponse<string>> SendConfirmationEmailAsync(Customer user, string appBaseUrl)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var confirmationLink = $"{appBaseUrl}/confirm-email?userId={user.Id}&token={encodedToken}";

            // TODO: Send confirmationLink via email

            return new GenericResponse<string>
            {
                Success = true,
                Message = "Confirmation email sent.",
                Content = confirmationLink
            };
        }

        public async Task<GenericResponse<string>> ConfirmEmailAsync(int userId, string encodedToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return new GenericResponse<string>
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "Email confirmed successfully." : "Email confirmation failed."
            };
        }

        public async Task<GenericResponse<string>> SendPasswordResetEmailAsync(string email, string appBaseUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var resetLink = $"{appBaseUrl}/reset-password?userId={user.Id}&token={encodedToken}";

            // TODO: Send resetLink via email

            return new GenericResponse<string>
            {
                Success = true,
                Message = "Password reset email sent.",
                Content = resetLink
            };
        }

        public async Task<GenericResponse<string>> ResetPasswordAsync(int userId, string encodedToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return new GenericResponse<string> { Success = false, Message = "User not found." };

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return new GenericResponse<string>
            {
                Success = result.Succeeded,
                Message = result.Succeeded ? "Password reset successful." : "Password reset failed."
            };
        }
    }
}
