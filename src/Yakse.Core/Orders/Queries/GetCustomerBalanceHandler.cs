using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Yakse.Core.Orders.Queries
{
    public class GetCustomerBalanceHandler : IRequestHandler<GetCustomerBalance, decimal>
    {
        private readonly IRepository _repository;

        public GetCustomerBalanceHandler(IRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<decimal> Handle(GetCustomerBalance request, CancellationToken cancellationToken)
        {
            // hack: builds balance based off a starting position of 10k and subtracts all orders
            // orders may no yet be placed and purchase price may not be bid price
            // also maybe better to store balance built from an order placed event rather than
            // running calculation everytime
            var startingBalance = 10000m;

            var customerOrders = await _repository.Find<Order>(x => x.CustomerId == request.CustomerId);
            var totalOrderAmount = customerOrders.Select(x => x.Quantity * x.BidPrice).Sum();

            var balance = startingBalance - totalOrderAmount;

            return balance;
        }
    }
}