using Application.Interfaces.IRepository;
using Domain.Entities;
using Persistence.Repository;
using Presistance.Context;

namespace Presistance.Repository
{
    internal class PaymentRepository(ApplicationDbContext _dbContext) : Repository<Payment>(_dbContext), IPaymentRepository
    {
    }
}
