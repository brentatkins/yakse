using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yakse.Core.Queries;

namespace Yakse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<StockPriceDto>> Get()
        {
            var prices = await _mediator.Send(new StockPriceRequest());

            return prices;
        }
    }
}