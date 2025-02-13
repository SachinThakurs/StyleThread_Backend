using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Repository
{
    internal class SizeRepository(ApplicationDbContext _dbContext) : Repository<Size>(_dbContext), ISizeRepository
    {
    }
}
