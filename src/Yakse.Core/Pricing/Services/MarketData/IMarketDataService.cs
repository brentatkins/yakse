using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yakse.Core.Pricing.Services.MarketData
{
    public interface IMarketDataService
    {
        Task<IEnumerable<StockTick>> GetIntradayPrice(string[] symbols);
    }
    
    public record StockTick(string Symbol, decimal Open, decimal High, decimal Low, decimal Last, DateTime Date);
}