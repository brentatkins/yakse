using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing;

namespace Yakse.Core.Orders.Commands
{
    public class PlaceOrderCommandHandler : AsyncRequestHandler<PlaceOrderCommand>
    {
        private readonly IRepository _repository;

        public PlaceOrderCommandHandler(IRepository repository)
        {
            _repository = repository;
        }
        
        protected override async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.PlaceOrder(request.CustomerId, request.Symbol, request.Quantity, request.BidPrice);
            await _repository.Insert(order);

            var stock = (await _repository.Find<Stock>(x => x.Symbol == request.Symbol)).Single();
            if (stock.LatestPrice != null && stock.LatestPrice.Last < request.BidPrice)
            {
                order.CompleteOrder(stock.LatestPrice.Last);
                await _repository.Update(order);
            }
        }
    }
}