using Application.TimeKeep.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustInTimeWeb.Controllers
{
    [Route("api/[controller]")]
    public class TimekeepController : BaseApiController
    {
        [HttpGet("monthly")]
        public async Task<ActionResult> GetMonthlyHours()
        {
            return Ok(await Mediator.Send(new MonthlyHoursQuery()));
        }
    }
}