using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repository
{
    internal class CartRepository(ApplicationDbContext _dbContext) : Repository<Cart>(_dbContext), ICartRepository
    {
        //public async Task<Cart?> GetCartByCustomerIdAsync(string customerId, CancellationToken cancellationToken)
        //{
        //    return await _dbContext.Cart
        //        .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);
        //}
    }
}
