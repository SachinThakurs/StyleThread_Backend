using Application.DTO;
using Application.Features.Command.GenericCommands;
using FluentValidation;

namespace Application.Features.Brand.Command
{
    public class CreateCartCommandValidator : AbstractValidator<GenericCreateCommand<CartDto, CartDto>>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Entity.CartId).NotEmpty().NotNull().WithMessage("Cart Id is required.");
            RuleFor(x => x.Entity.ProductId).NotEmpty().NotNull().WithMessage("Product Id is required.");
            RuleFor(x => x.Entity.Quantity).NotEmpty().NotNull().WithMessage("Quantity is required.");
            RuleFor(x => x.Entity.CustomerId).NotEmpty().NotNull().WithMessage("Quantity is required.");
        }
    }
}
