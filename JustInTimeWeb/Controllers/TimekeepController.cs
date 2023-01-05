using Application.TimeKeep.Commands;
using Application.TimeKeep.Queries;
using Microsoft.AspNetCore.Mvc;

namespace JustInTimeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimekeepController : BaseApiController
    {
        [HttpGet("monthly")]
        public async Task<ActionResult> GetMonthlyHours()
        {
            return Ok(await Mediator.Send(new MonthlyHoursQuery()));
        }

        [HttpGet("recent")]
        public async Task<ActionResult> GetRecentLogs()
        {
            return Ok(await Mediator.Send(new RecentLogsQuery()));
        }

        [HttpPost("in")]
        public async Task<ActionResult> PunchIn(PunchInCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("out")]
        public async Task<ActionResult> PunchOut(PunchOutCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("startbreak")]
        public async Task<ActionResult> StartBreak(StartBreakCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("endbreak")]
        public async Task<ActionResult> EndBreak(EndBreakCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}