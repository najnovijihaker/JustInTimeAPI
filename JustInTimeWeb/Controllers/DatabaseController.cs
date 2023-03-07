using Microsoft.AspNetCore.Mvc;

namespace JustInTimeWeb.Controllers
{
    // this controller is not made for uses inside of JustInTimeClient, but for 3rd party applications
    [NonController] // disable this controller until the feature is finished
    [ApiController]
    [Route("[controller]/")]
    public class DatabaseController : BaseApiController
    {
    }
}