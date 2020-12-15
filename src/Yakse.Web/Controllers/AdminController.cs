using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Yakse.Core.Pricing.Commands;

namespace Yakse.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class AdminController : ControllerBase
    {
        private readonly ISender _mediator;

        public AdminController(ISender mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Route("load/{count:int}")]
        public async Task Load(int count) {
            await _mediator.Send(new LoadStocksCommand(count));
        }
    }
}