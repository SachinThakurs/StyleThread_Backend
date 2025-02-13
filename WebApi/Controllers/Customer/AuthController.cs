﻿//using Application.Features.Command.BrandCommand;
//using Application.Features.Product.Command;
//using Application.Features.Product.Queries;
using Application.DTO;
using Application.Features.Command;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] GenericCreateCommand<CustomerDto, GenericResponse<string>> userData, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(userData, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle other errors
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Content = "Error"
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] GenericCreateCommand<LoginRequestDto, GenericResponse<string>> userCredentials, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(userCredentials, cancellationToken));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCustomer(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCustomersQuery(), cancellationToken));

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(GenericUpdateCommand<CustomerDto, GenericResponse<string>> updateProduct, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateProduct, cancellationToken));
        }
        [Authorize]
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(GenericDeleteCommand<CustomerDto, GenericResponse<string>> customerId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(customerId, cancellationToken));
        }

    }
}
