using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;

namespace Presistance.Repository
{
    internal class CategoryRepository(ApplicationDbContext _dbContext) : Repository<Category>(_dbContext), ICategoryRepository
    {
    }
}
