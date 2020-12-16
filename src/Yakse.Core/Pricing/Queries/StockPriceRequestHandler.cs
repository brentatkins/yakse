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
        private readonly IRepository _repository;

        public StockPriceRequestHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StockPriceDto>> Handle(StockPriceRequest request,
            CancellationToken cancellationToken)
        {
            var stocks = await _repository.All<Stock>();

            var prices = stocks
                .Where(x => x.LatestPrice is not null)
                .Select(s => new StockPriceDto(s.Symbol,
                        s.LatestPrice!.Last,
                        this.GetDelta(s.LatestPrice.Last, s.LatestPrice.Open),
                        this.GetDeltaRatio(s.LatestPrice.Last, s.LatestPrice.Open), 
                        s.LatestPrice.AsAtDate));

            return prices;
        }

        private decimal GetDelta(decimal last, decimal open) => last - open;

        private decimal GetDeltaRatio(decimal last, decimal open) => (last - open) / open;
    }
}