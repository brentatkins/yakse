using System.Collections.Generic;
using MediatR;

namespace Yakse.Core.Queries
{
    public class StockPriceRequest : IRequest<IEnumerable<StockPriceDto>>
    {
        
    }

    public record StockPriceDto(string Symbol, decimal LastPrice, decimal Delta, decimal DeltaRatio);
}