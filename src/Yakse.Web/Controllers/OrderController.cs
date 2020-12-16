using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yakse.Core.Orders.Commands;
using Yakse.Core.Orders.Queries;
using Yakse.Web.Models;

namespace Yakse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        // hack: no using any auth and assuming a single customer for now
        private const string HardcodedCustomerId = "1";

        private readonly ISender _mediator;

        public OrderController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> Get()
        {
            return await _mediator.Send(new GetOrderHistory(HardcodedCustomerId));
        }
        
        [HttpPost]
        public async Task PlaceOrder(PlaceOrderRequest placeOrder)
        {
            var command = new PlaceOrderCommand(HardcodedCustomerId, placeOrder.Symbol, placeOrder.Quantity,
                placeOrder.BidPrice);
            await _mediator.Send(command);
        }
    }
}