using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AlsacWebApiCore.DTOs;
using AlsacWebApiCore.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

namespace AlsacWebApiCore.Controllers
{
    /// <summary>
    /// Provides an endpoint that returns information about how
    /// the api is configured.
    /// </summary>
    [Route("v1/config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IHostingEnvironment _env;
        public static Func<IConnectionFactory, IHostingEnvironment, ConfigResponseDto> GetConfig { get; set; } = GetConfigDefault;

        public ConfigController(IConnectionFactory connectionFactory, IHostingEnvironment env)
        {
            _connectionFactory = connectionFactory;
            _env = env;
        }

        /// <summary>
        /// Returns information about the running context and current
        /// settings being used by this API.
        /// </summary>
        /// <returns>
        /// Returns a Json object (ConfigResponseDto).  Has information
        /// about what version of .Net Core runtime we are running in,
        /// what flavor and version of the server O/S we are running in,
        /// what database instance we are connecting to, and what the
        /// minimum log level is currently set to.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<ConfigResponseDto> Config()
        {
            return Ok(GetConfig(_connectionFactory, _env));
        }

        /// <summary>
        /// Set the minimum logging level for this set of APIs
        /// </summary>
        /// <param name="level">
        /// New minimum log level, should be one of the following:
        /// Verbose, Debug, Information, Warning, Error, Fatal
        /// </param>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult MinimumLogLevel(string level)
        {
            if (Enum.TryParse<LogEventLevel>(level, true, out var logMinimumLevel))
            {
                LogLevelControl.Switch.MinimumLevel = logMinimumLevel;
                return Ok();
            }
            return BadRequest();
        }

        public static ConfigResponseDto GetConfigDefault(IConnectionFactory connectionFactory, IHostingEnvironment env)
        {
            var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
            var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            var netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
            var version = "Microsoft.NETCore.App " + assemblyPath[netCoreAppIndex + 1];

            return new ConfigResponseDto
            {
                DbServer = connectionFactory?.GetDbServerNames(),
                AspNetVersion = version,
                OsVersion = RuntimeInformation.OSDescription,
                LogMinimumLevel = LogLevelControl.Switch.MinimumLevel,
                Environment = env.EnvironmentName
            };
        }
    }
}
