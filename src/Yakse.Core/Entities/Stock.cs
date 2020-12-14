namespace Yakse.Core.Entities
{
    public class Stock : BaseEntity
    {
        public Stock(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; private set; }
    }
}