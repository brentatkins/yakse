using MediatR;

namespace Yakse.Core.Pricing.Events
{
    public class StockPriceUpdatedEvent : INotification
    {
        public StockPriceUpdatedEvent(string symbol, decimal lastTradePrice)
        {
            Symbol = symbol;
            LastTradePrice = lastTradePrice;
        }

        public string Symbol { get; }

        public decimal LastTradePrice { get; }
    }
}