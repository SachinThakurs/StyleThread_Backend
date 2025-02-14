

using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;

namespace Presistance.Repository
{
    internal class FabricCareRepository(ApplicationDbContext _dbContext) : Repository<FabricCare>(_dbContext), IFabricCareRepository
    {
    }
}
