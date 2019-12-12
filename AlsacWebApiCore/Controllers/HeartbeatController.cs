using Microsoft.AspNetCore.Mvc;

namespace AlsacWebApiCore.Controllers
{
    [ApiController]
    [Route("v1/heartbeat")]
    public class HeartbeatController : Controller
    {
        /// <summary>
        /// Used to see if routing is correct and most basic response is available.
        /// No services are called or checked (see #/v1/status for that).  This just
        /// confirms that the api should be available at the base address being used.
        /// </summary>
        /// <returns>
        /// OK
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Heartbeat()
        {
            return Ok("OK");
        }
    }
}
