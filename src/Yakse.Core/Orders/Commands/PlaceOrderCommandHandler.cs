using System.Threading;
using System.Threading.Tasks;
using MediatR;

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
            // question: should the order be rejected if customer balance is too low?
            
            var order = Order.PlaceOrder(request.CustomerId, request.Symbol, request.Quantity, request.BidPrice);
            await _repository.Insert(order);
        }
    }
}