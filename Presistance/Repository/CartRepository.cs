using Application.DTO;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;
using Razorpay.Api;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Repository
{
    internal class CartRepository(ApplicationDbContext _dbContext) : Repository<Cart>(_dbContext), ICartRepository
    {
        public async Task<List<CartDto>> GetAllItemByCustomerId(string CustomerId, CancellationToken cancellationToken)
        {
            List<CartDto> cartItems = await _dbContext.Cart
                .Where(x => x.CustomerId == CustomerId)
                .Select(x => new CartDto
                {
                    CartId = x.CartId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    CustomerId = x.CustomerId
                })
                .ToListAsync(cancellationToken);

            return cartItems;
        }
    }
}
