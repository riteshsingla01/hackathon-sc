using System;
using System.Linq;
using System.Threading.Tasks;
using AlsacWebApiCore.DTOs;
using AlsacWebApiCore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlsacWebApiCore.Controllers
{
    [ApiController]
    [Route("v1/health")]
    public class HealthController : Controller
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConfiguration _configuration;

        public static Func<IConnectionFactory, IConfiguration, Task<SubsystemStatusResponseDto>> GetHealth { get; set; } = GetHealthDefault;


        public HealthController(IConnectionFactory connectionFactory, IConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// This request gets a list of all the subsystems used by this service and their status, Up or Down.
        /// Examples of sub-systems are database connections or external services.
        /// You should enumerate through all sub-systems and perform some test that will indicate the current state of the service.
        /// This service should return a HTTP 200 response code if all systems are responsive, or a 417 if one of more sub-systems are not responsive.
        /// The body should be a list of all subsystems (one per line) followed by the status: Up or Down.
        /// </summary>
        /// <returns>
        /// OK
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SubsystemStatusResponseDto>> Health()
        {
            var result = await GetHealth(_connectionFactory, _configuration);
            if (result.OverallSystemUp)
                return Ok(result);
            else
                return NotFound(result);
        }

        public static async Task<SubsystemStatusResponseDto> GetHealthDefault(IConnectionFactory connectionFactory, IConfiguration configuration)
        {
            var statusList = await ConnectionHealthCheck.CheckConnections(connectionFactory);

            if (statusList.Count == 0)
                statusList.Add(new SubsystemStatusResponseItem { SubsystemName = "No Subsystems", SubsystemUp = true });

            return new SubsystemStatusResponseDto() {Subsystems = statusList, OverallSystemUp = statusList.All(x => x.SubsystemUp)};
        }
    }
}
