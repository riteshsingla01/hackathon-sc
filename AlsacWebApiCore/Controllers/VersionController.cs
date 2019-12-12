using System;
using AlsacWebApiCore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlsacWebApiCore.Controllers
{
    /// <summary>
    /// Provides a single service that provides information about the version
    /// and build date of the service.
    /// </summary>
    [ApiController]
    [Route("v1/version")]
    public class VersionController : Controller
    {
        private readonly IConfiguration _configuration;
        public static Func<IConfiguration, VersionResponseDto> GetVersion { get; set; } = GetVersionDefault;

        public VersionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Fetch the version and build date of the service.
        /// </summary>
        /// <returns>
        /// Returns the version number and build date of the service.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<VersionResponseDto> Version()
        {
            return Ok(GetVersion(_configuration));
        }

        public static VersionResponseDto GetVersionDefault(IConfiguration configuration) =>
            new VersionResponseDto
            {
                VersionNumber = configuration.GetValue<string>("versionInfo:versionNumber"),
                BuildDate = configuration.GetValue<string>("versionInfo:buildDate")
            };
    }
}
