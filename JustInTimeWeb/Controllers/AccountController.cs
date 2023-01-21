using Application.Account.Commands;
using Application.Account.Queries;
using Application.Common.Dtos;
using Application.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustInTimeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateCommand request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        //User reset password by email
        [HttpPost("reset")]
        public async Task<ActionResult> Reset(ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        //manually verifies account
        [HttpPost("manually-activate")]
        [Authorize]
        public async Task<ActionResult> ManuallyActivate(ManuallyActivateCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<AuthResponseDto>> CreateAccount(CreateAccountCommand command)

        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult> LoadAccount(string username)
        {
            var result = await Mediator.Send(new AccountQuery(username));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("update")]
        public async Task<ActionResult> UpdateAccount(UpdateAccountCommand command) //finds account by ID
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<ActionResult> DeleteAccount(DeleteAccountCommand command) //finds account by username
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult> GetAllAccounts()
        {
            var result = await Mediator.Send(new AccountsQuery());

            return Ok(result);
        }

        // Returns hours worked on project with projectId
        [Authorize]
        [HttpGet("hours/{accountId}/{projectId}")]
        public async Task<ActionResult> GetHoursForProject(int accountId, int projectId)
        {
            var result = await Mediator.Send(new AccountTotalHoursQuery(accountId, projectId));

            return Ok(result);
        }

        // Returns hours for all projects user has contributed to
        [Authorize]
        [HttpGet("statistics/{accountId}")]
        public async Task<ActionResult> GetAccountStatistics(int accountId)
        {
            var result = await Mediator.Send(new AccountStatisticsQuery(accountId));

            return Ok(result);
        }

        //unlock account
        [HttpPost("unlock")]
        [Authorize]
        public async Task<ActionResult> UnlockAccount(UnlockAccountCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        //Lock account
        [HttpPost("lock")]
        [Authorize]
        public async Task<ActionResult> LockAccount(LockAccountCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}