using MediatR;

namespace Yakse.Core.Commands
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