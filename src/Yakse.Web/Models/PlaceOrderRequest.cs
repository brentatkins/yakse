namespace Yakse.Web.Models
{
    public class PlaceOrderRequest
    {
        public PlaceOrderRequest(string symbol, int quantity, decimal bidPrice)
        {
            Symbol = symbol;
            Quantity = quantity;
            BidPrice = bidPrice;
        }

        public string Symbol { get; }
        public int Quantity { get; }
        public decimal BidPrice { get; }
    }
}