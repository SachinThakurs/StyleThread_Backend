using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repository
{
    internal class BrandRepository(ApplicationDbContext _dbContext) : Repository<Brand>(_dbContext), IBrandRepository
    {
    }
}
