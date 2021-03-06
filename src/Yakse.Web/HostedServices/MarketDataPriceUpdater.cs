using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yakse.Core.Pricing.Commands;

namespace Yakse.Web.HostedServices
{
    public class MarketDataPriceUpdater : BackgroundService
    {
        private readonly ILogger<MarketDataPriceUpdater> _logger;
        private readonly ISender _mediator;

        public MarketDataPriceUpdater(ILogger<MarketDataPriceUpdater> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                await this.UpdatePrices(cancellationToken);
                await Task.Delay(5000, cancellationToken);
            }
        }

        private async Task UpdatePrices(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating stock prices");

            var orderHistory = await _mediator.Send(new UpdateTickers());
            _logger.LogInformation(orderHistory.ToString());
        }
    }
}