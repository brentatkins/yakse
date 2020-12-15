namespace Yakse.Core.Orders
{
    public class Order : BaseEntity
    {
        private Order(string customerId, string symbol, int quantity, decimal bidPrice)
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

        public static Order PlaceOrder(string customerId, string symbol, int quantity, decimal bidPrice)
        {
            return new Order(customerId, symbol, quantity, bidPrice);
        }
    }
}