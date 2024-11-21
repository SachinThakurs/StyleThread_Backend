using Application.Interfaces.IRepository;
using Domain.Entities;
using Presistance.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;

namespace Presistance.Repository
{
    internal class ProductVariantRepository(ApplicationDbContext _dbContext) : Repository<ProductVariant>(_dbContext), IProductVariantRepository
    {
        public async Task<ICollection<ProductVariant>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.ProductVariants
                .Where(variant => variant.ProductId == productId)
                .ToListAsync(cancellationToken); // Ensure you are using ToListAsync
            return data;
        }


    }
}
