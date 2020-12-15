using MediatR;

namespace Yakse.Core.Orders.Queries
{
    public class GetCustomerBalance : IRequest<decimal>
    {
        public GetCustomerBalance(string customerId)
        {
            CustomerId = customerId;
        }

        public string CustomerId { get; }
    }
}