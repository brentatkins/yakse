using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yakse.Core.Services.MarketData
{
    public class RandomDataMarketDataService : IMarketDataService
    {
        private const int UpdateFrequencyInSeconds = 10;

        private readonly Dictionary<string, (DateTime LastTradeDate, decimal Open, decimal High, decimal Low, decimal Last)> _tickers = new Dictionary<string, (DateTime LastTradeDate, decimal Open, decimal High, decimal Low, decimal Last)>();
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

            // change this to generate prices automatically, rather than when ever prices are requested
            if (DateTime.UtcNow.Subtract(lastTradeDate).Seconds < UpdateFrequencyInSeconds) {
                return new StockTick(symbol, open, high, low, last);
            }

            var next = GetNewMarketPrice(last);

            if (high < next) {
                high = next;
            }
            if (low > next) {
                low = next;
            }

            _tickers[symbol] = (DateTime.UtcNow, open, high, low, next);

            return new StockTick(symbol, open, high, low, next);
        }

        private void AddNewTicker(string symbol){
            var randomStartingPrice = Rand.NextDecimal(2m, 200m);
                _tickers[symbol] = (DateTime.UtcNow, randomStartingPrice, randomStartingPrice, randomStartingPrice, randomStartingPrice);
        }

        private decimal GetNewMarketPrice(decimal lastPrice) {
            var change = (Rand.Next(200) - 100) / 1000m;
            var newPrice = lastPrice +  (lastPrice * change);
            return newPrice;
        }
    }
}