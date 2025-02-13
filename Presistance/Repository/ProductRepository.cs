using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;
using Microsoft.EntityFrameworkCore;


namespace Presistance.Repository
{
    internal class ProductRepository(ApplicationDbContext _dbContext) : Repository<Product>(_dbContext), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllWithVariantsAndSizesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Product
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.ProductVariantSizes)
                        .ThenInclude(s => s.Size)
                .ToListAsync(cancellationToken);
        }
    }
}
