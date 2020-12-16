using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Yakse.Core.Pricing.Queries
{
    public class GetStockPrices : IRequest<IEnumerable<StockPriceDto>>
    {
        
    }

    public record StockPriceDto(string Symbol, decimal LastPrice, decimal Delta, decimal DeltaRatio, DateTime PriceDate);
    
    public class GetStockPricesHandler : IRequestHandler<GetStockPrices, IEnumerable<StockPriceDto>>
    {
        private readonly IRepository _repository;

        public GetStockPricesHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StockPriceDto>> Handle(GetStockPrices request,
            CancellationToken cancellationToken)
        {
            var stocks = await _repository.All<Stock>();

            var prices = stocks
                .Where(x => x.LatestPrice is not null)
                .OrderBy(x => x.Symbol)
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