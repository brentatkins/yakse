using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Yakse.Core.Entities;

namespace Yakse.Core.Commands
{
    public class LoadStocksCommandHandler : AsyncRequestHandler<LoadStocksCommand>
    {
        private static readonly Random Rand = new();
        private readonly IRepository _repository;

        public LoadStocksCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        protected override Task Handle(LoadStocksCommand request, CancellationToken cancellationToken)
        {
            var newStockCodes = CreateStockCodes(request.Count);
            var stocks = newStockCodes
                .Select(code => new Stock(code))
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