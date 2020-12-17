using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Xunit;
using Yakse.Core.Pricing;
using Yakse.Core.Pricing.Queries;
using Yakse.Core.Pricing.Services.MarketData;

namespace Yakse.Core.Tests.Pricing
{
    public class GetStockPriceTests : BaseTest
    {

        [Fact]
        public async Task GetPrice_ShouldShowDelta()
        {
            // arrange
            var symbol = "aaa";
            var stock = Stock.Create(symbol);
            await Repository.Insert(stock);

            var open = 100m;
            var last = 110m;
            stock.RecordPrice(new StockTick(symbol, open, open, open, last, DateTime.Now));
            await Repository.Update(stock);
            
            // act
            var prices = await Mediator.Send(new GetStockPrices());

            // assert
            prices.Should().ContainSingle(x => x.Symbol == symbol);
            var price = prices.Single(x => x.Symbol == symbol);

            price.LastPrice.Should().Be(last);
            price.Delta.Should().Be(10);
            price.DeltaRatio.Should().Be(0.1m);
        }
        
    }
}