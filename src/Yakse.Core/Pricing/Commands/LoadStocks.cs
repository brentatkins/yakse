using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Yakse.Core.Pricing.Commands
{
    public class LoadStocks : IRequest
    {
        public int Count { get; }

        public LoadStocks(int count)
        {
            Count = count;
        }
    }
    
    public class LoadStocksHandler : AsyncRequestHandler<LoadStocks>
    {
        private static readonly Random Rand = new();
        private readonly IRepository _repository;

        public LoadStocksHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override Task Handle(LoadStocks request, CancellationToken cancellationToken)
        {
            var newStockCodes = CreateStockCodes(request.Count);
            var stocks = newStockCodes
                .Select(Stock.Create)
                .Select(stock => _repository.Insert(stock));

            return Task.WhenAll(stocks);
        }

        private string[] CreateStockCodes(int numberOfStocks)
        {
            return Enumerable.Range(1, numberOfStocks)
                .Select(x => GenerateRandomStockCode())
                .ToArray();
        }

        private string GenerateRandomStockCode()
        {
            var code = $"{(char) Rand.Next(65, 90)}{(char) Rand.Next(65, 90)}{(char) Rand.Next(65, 90)}";
            return code;
        }
    }
}