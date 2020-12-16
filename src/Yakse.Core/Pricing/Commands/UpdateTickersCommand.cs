using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Pricing.Events;
using Yakse.Core.Pricing.Services.MarketData;

namespace Yakse.Core.Pricing.Commands
{
    public class UpdateTickersCommand : IRequest
    {
    }

    public class UpdateTickersCommandHandler : AsyncRequestHandler<UpdateTickersCommand>
    {
        private readonly IPublisher _mediator;
        private readonly IMarketDataProvider _marketDataProvider;
        private readonly IRepository _repository;

        public UpdateTickersCommandHandler(IPublisher mediator, IMarketDataProvider marketDataProvider, IRepository repository)
        {
            _mediator = mediator;
            _marketDataProvider = marketDataProvider;
            _repository = repository;
        }

        protected override async Task Handle(UpdateTickersCommand request, CancellationToken cancellationToken)
        {
            var stocks = await _repository.All<Stock>();
            var stockSymbols = stocks.Select(s => s.Symbol).ToArray();

            var tickers = await _marketDataProvider.GetIntradayPrice(stockSymbols);
            
            foreach (var stockTick in tickers)
            {
                var stock = stocks.Single(x => x.Symbol == stockTick.Symbol);
                stock.RecordPrice(stockTick);
                await _repository.Update(stock);
                await _mediator.Publish(new StockPriceUpdatedEvent(stock.Symbol, stockTick.Last));
            }
        }
    }
}