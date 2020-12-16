using System;
using Yakse.Core.Pricing;

namespace Yakse.Core.Orders
{
    public class Order : BaseEntity
    {
        public enum OrderStatus
        {
            Pending,
            Executed
        }
        
        private Order(string customerId, string symbol, int quantity, decimal bidPrice, DateTime orderDate)
        {
            CustomerId = customerId;
            Symbol = symbol;
            Quantity = quantity;
            BidPrice = bidPrice;
            OrderDate = orderDate;
            Status = OrderStatus.Pending;
        }

        public string CustomerId { get; }

        public string Symbol { get; }

        public int Quantity { get; }

        public decimal BidPrice { get; }

        public DateTime OrderDate { get; }
        
        public decimal? TradePrice { get; private set; }
        
        public DateTime? TradeDate { get; private set; }
        
        public OrderStatus Status { get; private set; }

        public static Order PlaceOrder(string customerId, string symbol, int quantity, decimal bidPrice)
        {
            return new Order(customerId, symbol, quantity, bidPrice, DateTime.UtcNow);
        }

        public void CompleteOrder(decimal orderPrice)
        {
            TradeDate = DateTime.UtcNow;
            TradePrice = orderPrice;
            Status = OrderStatus.Executed;
        }
    }
}