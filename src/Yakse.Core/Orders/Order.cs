using System;

namespace Yakse.Core.Orders
{
    public class Order : BaseEntity
    {
        private Order(string customerId, string symbol, int quantity, decimal bidPrice, DateTime orderDate)
        {
            CustomerId = customerId;
            Symbol = symbol;
            Quantity = quantity;
            BidPrice = bidPrice;
            OrderDate = orderDate;
        }

        public string CustomerId { get; }

        public string Symbol { get; }

        public int Quantity { get; }

        public decimal BidPrice { get; }
        
        public DateTime OrderDate { get; }

        public static Order PlaceOrder(string customerId, string symbol, int quantity, decimal bidPrice)
        {
            return new Order(customerId, symbol, quantity, bidPrice, DateTime.UtcNow);
        }
    }
}