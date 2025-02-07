
using Application.DTO;
using Application.Features.Command.GenericCommands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using MediatR;
using System;
using System.Threading;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Payment
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] GenericCreateCommand<PaymentLinkRequestDTO, GenericResponse<string>> paymentRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(paymentRequest, cancellationToken));
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] GenericCreateCommand<PaymentVerificationRequestDTO, GenericResponse<PaymentVerificationRequestDTO>> paymentVerificationRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(paymentVerificationRequest, cancellationToken));
        }
    }
}
