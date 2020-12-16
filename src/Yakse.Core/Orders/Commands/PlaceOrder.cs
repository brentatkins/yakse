using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing;

namespace Yakse.Core.Orders.Commands
{
    public class PlaceOrder : IRequest
    {
        public PlaceOrder(string customerId, string symbol, int quantity, decimal bidPrice)
        {
            CustomerId = customerId;
            Symbol = symbol;
            Quantity = quantity;
            BidPrice = bidPrice;
        }

        public string CustomerId { get; }
        
        public string Symbol { get; }

        public int Quantity { get; }

        public decimal BidPrice { get; }
    }
    
    public class PlaceOrderHandler : AsyncRequestHandler<PlaceOrder>
    {
        private readonly IRepository _repository;

        public PlaceOrderHandler(IRepository repository)
        {
            _repository = repository;
        }
        
        protected override async Task Handle(PlaceOrder request, CancellationToken cancellationToken)
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