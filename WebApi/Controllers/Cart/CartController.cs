using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAllCartItems(string customerId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCartItemsQuery(customerId), cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> AddItemInCart(GenericCreateCommand<CartDto, GenericResponse<string>> cartRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(cartRequest, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(GenericUpdateCommand<CartDto, GenericResponse<string>> updateCartRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateCartRequest, cancellationToken));
        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteCartItem(GenericDeleteCommand<CartDto, GenericResponse<string>> cartId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(cartId, cancellationToken));
        }
    }
}
