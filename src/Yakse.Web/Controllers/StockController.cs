using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yakse.Core.Pricing.Queries;

namespace Yakse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ISender _mediator;

        public StockController(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<StockPriceDto>> Get()
        {
            var prices = await _mediator.Send(new GetStockPrices());

            return prices;
        }
    }
}