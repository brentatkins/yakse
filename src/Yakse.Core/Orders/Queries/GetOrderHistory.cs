using System;
using System.Collections.Generic;
using MediatR;

namespace Yakse.Core.Orders.Queries
{
    public class GetOrderHistory : IRequest<IEnumerable<OrderDto>>
    {
        public string CustomerId { get; }

        public GetOrderHistory(string customerId)
        {
            CustomerId = customerId;
        }
    }

    public record OrderDto(string CustomerId, string Symbol, int Quantity, decimal BidPrice, DateTime OrderDate);
}