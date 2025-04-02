
namespace Application.Interfaces.IRepository
{
    public interface IUnitOfWork
    {
        IBrandRepository brandRepository { get; }
        ICategoryRepository categoryRepository { get; }
        ICustomerRepository customerRepository { get; }
        IProductRepository productRepository { get; }
        IProductVariantRepository productVariantRepository { get; }
        IFitRepository fitRepository { get; }
        IFabricRepository fabricRepository { get; }
        IFabricCareRepository fabricCareRepository { get; }
        ISleeveRepository sleeveRepository { get; }
        INeckTypeRepository neckTypeRepository { get; }
        IColorRepository colorRepository { get; }
        ISizeRepository sizeRepository { get; }
        IProductVariantSize productVariantSize { get; }
        ICartRepository cartRepository { get; }
    }
}
