using Application.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Presistance.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presistance.Repository
{
    internal class ProductVariantSize(ApplicationDbContext dbContext) : IProductVariantSize
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<ICollection<Domain.Entities.ProductVariantSize>> GetByProductVariantSizeIdAsync(int productVariantId, CancellationToken cancellationToken)
        {
            var data = await _dbContext.ProductVariantSizes
                .Where(variant => variant.ProductVariantId == productVariantId)
                .ToListAsync(cancellationToken);

            return data ?? new List<Domain.Entities.ProductVariantSize>();
        }
    }
}
