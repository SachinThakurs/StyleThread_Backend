using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        //Task<Cart?> GetCartByCustomerIdAsync(string customerId, CancellationToken cancellationToken);
    }
}
