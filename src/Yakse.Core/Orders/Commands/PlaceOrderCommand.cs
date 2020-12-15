using MediatR;

namespace Yakse.Core.Orders.Commands
{
    public class PlaceOrderCommand : IRequest
    {
        public PlaceOrderCommand(string customerId, string symbol, int quantity, decimal bidPrice)
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
}