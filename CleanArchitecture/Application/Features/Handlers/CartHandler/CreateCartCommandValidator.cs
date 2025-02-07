//using Application.Features.Cart.Command;
//using FluentValidation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.CartCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Brand.Command
//{
//    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
//    {
//        public CreateCartCommandValidator()
//        {
//            RuleFor(x => x.Cart.ProductId).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
//        }
//    }
//}
