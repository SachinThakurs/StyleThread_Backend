using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAllBrandsQuery : IRequest<IEnumerable<Domain.Entities.Brand>>
    {
        internal class GetAllProductsQureyHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllBrandsQuery, IEnumerable<Domain.Entities.Brand>>
        {
            public async Task<IEnumerable<Domain.Entities.Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.brandRepository.GetAllAsync(cancellationToken);
            }
        }
    }
}
