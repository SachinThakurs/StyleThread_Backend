//using Application.Features.Cart.Command;
//using Application.Interfaces;
//using AutoMapper;
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Application.Features.Command.CartCommand;
//using static Application.Features.Command.WishlistCommand;

//namespace Application.Features.Handlers.CartHandler
//{
//    internal class CreateCartCommandHandler(IApplicationDbcontext dbcontext, IMapper mapper) : IRequestHandler<CreateCartCommand, int>
//    {

//        public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
//        {
//            var cart = mapper.Map<Domain.Entites.Cart>(request);

//            await dbcontext.Cart.AddAsync(cart);
//            await dbcontext.SaveChangesAsync();
//            return cart.CartId;
//        }
//    }
//}
