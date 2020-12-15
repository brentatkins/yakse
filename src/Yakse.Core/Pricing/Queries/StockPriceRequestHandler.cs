using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing.Services.MarketData;

namespace Yakse.Core.Pricing.Queries
{
    public class StockPriceRequestHandler : IRequestHandler<StockPriceRequest, IEnumerable<StockPriceDto>>
    {
        private readonly IMarketDataService _marketDataService;
        private readonly IRepository _repository;

        public StockPriceRequestHandler(IRepository repository, IMarketDataService marketDataService)
        {
            _repository = repository;
            _marketDataService = marketDataService;
        }

        public async Task<IEnumerable<StockPriceDto>> Handle(StockPriceRequest request,
            CancellationToken cancellationToken)
        {
            var stocks = await _repository.All<Stock>();
            var stockCodes = stocks.Select(s => s.Symbol).ToArray();

            var ticks = await _marketDataService.GetIntradayPrice(stockCodes);

            return ticks.Select(x =>
                new StockPriceDto(x.Symbol, x.Last, x.Last - x.Open, (x.Last - x.Open) / x.Open, x.Date));
        }
    }
}