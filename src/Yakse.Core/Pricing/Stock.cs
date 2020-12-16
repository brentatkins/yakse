using System;
using System.Collections.Generic;
using Yakse.Core.Pricing.Services.MarketData;

namespace Yakse.Core.Pricing
{
    public class Stock : BaseEntity
    {
        private Stock(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; private set; }
        
        public StockPrice? LatestPrice { get; private set; }

        public static Stock Create(string code)
        {
            return new Stock(code);
        }

        public void RecordPrice(StockTick stockTick)
        {
            LatestPrice = new StockPrice
            {
                Open = stockTick.Open,
                Last = stockTick.Last,
                AsAtDate = stockTick.Date,
                High = stockTick.High,
                Low = stockTick.Low
            };
        }

        public class StockPrice
        {
            public decimal Open { get; init; }
            
            public decimal High { get; init; } 
            
            public decimal Low { get; init; }
            
            public decimal Last { get; init; }
            
            public DateTime AsAtDate { get; init; }
        }
    }
}