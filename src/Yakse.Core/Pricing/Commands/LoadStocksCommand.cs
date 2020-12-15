using MediatR;

namespace Yakse.Core.Pricing.Commands
{
    public class LoadStocksCommand : IRequest
    {
        public int Count { get; }

        public LoadStocksCommand(int count)
        {
            Count = count;
        }
    }
}