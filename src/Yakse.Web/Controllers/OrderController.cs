using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yakse.Core.Orders.Commands;

namespace Yakse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ISender _mediator;

        public OrderController(ISender mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task PlaceOrder(PlaceOrderCommand placeOrder) {
            await _mediator.Send(placeOrder);
        }
    }
}