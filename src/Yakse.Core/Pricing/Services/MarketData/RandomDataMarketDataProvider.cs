using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yakse.Core.Pricing.Services.MarketData
{
    public class RandomDataMarketDataProvider : IMarketDataProvider
    {
        private const int UpdateFrequencyInSeconds = 5;

        private readonly Dictionary<string, (DateTime LastTradeDate, decimal Open, decimal High, decimal Low, decimal Last)> _tickers = new();
        private static readonly Random Rand = new Random();
        
        public Task<IEnumerable<StockTick>> GetIntradayPrice(string[] symbols)
        {
            // may need to think about chunking and rate limits for calls to a real 3rd party service
            return Task.FromResult(symbols.Select(GetStockTick));
        }

        private StockTick GetStockTick(string symbol)
        {
            if (!_tickers.ContainsKey(symbol)) {
                AddNewTicker(symbol);
            }

            var (lastTradeDate, open, high, low, last) =  _tickers[symbol];

            // randomly return the same value to simulate case where the a new price is not available
            // the old the value the less likely it is to be updated
            var priceAge = (DateTime.UtcNow - lastTradeDate).Seconds;
            var noNewValueProbability = priceAge switch
            {
                > 15 => 95,
                > 8 => 50,
                _ => 10
            };
            
            var noPriceAvailable = Rand.Next(100) <= noNewValueProbability;
            if (noPriceAvailable)
            {
                return new StockTick(symbol, open, high, low, last, lastTradeDate);
            }
            
            // to prevent prices updating too frequently, only on set frequency
            if (DateTime.UtcNow.Subtract(lastTradeDate).Seconds < UpdateFrequencyInSeconds) {
                return new StockTick(symbol, open, high, low, last, DateTime.UtcNow);
            }

            var next = GetNewMarketPrice(last);

            if (high < next) {
                high = next;
            }
            if (low > next) {
                low = next;
            }

            _tickers[symbol] = (DateTime.UtcNow, open, high, low, next);

            return new StockTick(symbol, open, high, low, next, DateTime.UtcNow);
        }

        private void AddNewTicker(string symbol){
            var randomStartingPrice = Rand.NextDecimal(2m, 200m);
            var lastPrice = GetNewMarketPrice(randomStartingPrice);
            _tickers[symbol] = (DateTime.UtcNow, randomStartingPrice, randomStartingPrice > lastPrice ? randomStartingPrice : lastPrice, randomStartingPrice < lastPrice ? randomStartingPrice : lastPrice, lastPrice);
        }

        private decimal GetNewMarketPrice(decimal lastPrice) {
            var change = (Rand.Next(200) - 100) / 1000m;
            var newPrice = lastPrice +  (lastPrice * change);
            return newPrice;
        }
    }
}