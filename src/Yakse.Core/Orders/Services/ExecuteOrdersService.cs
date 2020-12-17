using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing.Events;

namespace Yakse.Core.Orders.Services
{
    // bit of a contrived way to match orders execute trades. we're NOT trying to match bids to asks,
    // but just assuming that once the last trade price falls below bid price, order can be executed
    public class ExecuteOrdersService : INotificationHandler<StockPriceUpdatedEvent>
    {
        private readonly IRepository _repository;

        public ExecuteOrdersService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(StockPriceUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var matchingOrders = await _repository.Find<Order>(x => x.Status == Order.OrderStatus.Pending &&
                                                                    x.Symbol == @event.Symbol &&
                                                                    x.BidPrice >= @event.LastTradePrice);

            foreach (var order in matchingOrders)
            {
                // maybe best through mediatr pipeline rather than direct change on domain class
                order.CompleteOrder(@event.LastTradePrice);
                await _repository.Update(order);
            }
        }
    }
}