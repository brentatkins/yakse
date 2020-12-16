using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Yakse.Core.Orders.Queries
{
    public class GetOrderHistoryHandler : IRequestHandler<GetOrderHistory, IEnumerable<OrderDto>>
    {
        private readonly IRepository _repository;

        public GetOrderHistoryHandler(IRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<OrderDto>> Handle(GetOrderHistory request, CancellationToken cancellationToken)
        {
            var orders = await _repository.Find<Order>(x => x.CustomerId == request.CustomerId);
            
            return orders
                .OrderByDescending(x => x.OrderDate)
                .Select(o => new OrderDto(o.CustomerId, o.Symbol, o.Quantity, o.BidPrice, o.OrderDate, o.Status.ToString(), o.TradePrice, o.TradeDate));
        }
    }
}