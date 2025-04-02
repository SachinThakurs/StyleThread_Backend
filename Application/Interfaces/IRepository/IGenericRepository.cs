using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces.IRepository
{
    public interface IGenericRepository<T> : IRepository<T> where T : class
    {
    }

    public interface IColorRepository : IGenericRepository<Color> { }
    public interface ICategoryRepository : IGenericRepository<Category> { }
    public interface IBrandRepository : IGenericRepository<Brand> { }
    public interface ICustomerRepository : IGenericRepository<Customer> { }
    public interface IFabricCareRepository : IGenericRepository<FabricCare> { }
    public interface IFabricRepository : IGenericRepository<Fabric> { }
    public interface IFitRepository : IGenericRepository<Fit> { }
    public interface INeckTypeRepository : IGenericRepository<NeckType>{}
    public interface ISizeRepository : IGenericRepository<Size>{}
    public interface ISleeveRepository : IGenericRepository<Sleeve>{}
    public interface ICartRepository : IGenericRepository<Cart> 
    {
        Task<List<CartDto>> GetAllItemByCustomerId(string ProductId, CancellationToken cancellationToken);
    }
}
