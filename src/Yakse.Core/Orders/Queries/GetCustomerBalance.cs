using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing.Queries;

namespace Yakse.Core.Orders.Queries
{
    public class GetCustomerBalance : IRequest<BalanceDto>
    {
        public GetCustomerBalance(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }
    }

    public record BalanceDto(decimal CashBalance, decimal PortfolioBalance);
    
    public class GetCustomerBalanceHandler : IRequestHandler<GetCustomerBalance, BalanceDto>
    {
        private readonly IRepository _repository;
        private readonly ISender _mediator;

        public GetCustomerBalanceHandler(IRepository repository, ISender mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        
        public async Task<BalanceDto> Handle(GetCustomerBalance request, CancellationToken cancellationToken)
        {

            var executedOrders = await _repository
                .Find<Order>(x => x.CustomerId == request.CustomerId && x.Status == Order.OrderStatus.Executed);
                
            var cashBalance = GetCashBalance(executedOrders);
            var portfolioBalance = await GetPortfolioBalance(executedOrders);

            return new BalanceDto(cashBalance, portfolioBalance);
        }

        private decimal GetCashBalance(IEnumerable<Order> customerOrders)
        {
            // hack: doesn't support multiple customers
            // also recalculates balance with every request
            var startingBalance = 10000m;

            var totalOrderAmount = customerOrders
                .Select(x => x.Quantity * x.TradePrice!.Value)
                .Sum();

            var balance = startingBalance - totalOrderAmount;

            return balance;
        }
        
        private async Task<decimal> GetPortfolioBalance(IEnumerable<Order> orders)
        {
            var latestPrices = await _mediator.Send(new GetStockPrices());
            
            var portfolioBalance = orders
                .Select(x => x.Quantity * GetLastPrice(x.Symbol))
                .Sum();

            return portfolioBalance;

            decimal GetLastPrice(string symbol)
            {
                return latestPrices.Single(p => p.Symbol == symbol).LastPrice;
            }
        }
    }
}