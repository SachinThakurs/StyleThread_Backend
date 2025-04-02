using Application.DTO;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries
{
    public class GetAllCartItemsQuery : IRequest<IEnumerable<CartDto>>
    {
        public string CustomerId { get; set; }

        public GetAllCartItemsQuery(string customerId)
        {
            CustomerId = customerId;
        }

        internal class GetAllCartItemHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAllCartItemsQuery, IEnumerable<CartDto>>
        {
            public async Task<IEnumerable<CartDto>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    List<CartDto> fetchedData = await _unitOfWork.cartRepository.GetAllItemByCustomerId(request.CustomerId, cancellationToken);
                    if (fetchedData != null)
                        return fetchedData;
                    throw new Exception();
                }
                catch (Exception ex)
                {
                    throw;
                }
                
            }
        }
    }
}
