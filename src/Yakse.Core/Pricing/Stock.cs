namespace Yakse.Core.Pricing
{
    public class Stock : BaseEntity
    {
        private Stock(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; private set; }

        public static Stock Create(string code)
        {
            return new Stock(code);
        }
    }
}