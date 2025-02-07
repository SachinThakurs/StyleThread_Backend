using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;

namespace Presistance.Repository
{
    internal class ColorRepository(ApplicationDbContext _dbContext) : Repository<Color>(_dbContext), IColorRepository
    {
    }
}
