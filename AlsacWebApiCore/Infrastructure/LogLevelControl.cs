using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace AlsacWebApiCore.Infrastructure
{
    public static class LogLevelControl
    {
        public static LoggingLevelSwitch Switch { get; set; } = new LoggingLevelSwitch();
        public static string LogLevelConfigurationPath { get; set; } = "serilog:minimumLevel:default";

        public static LogEventLevel GetInitialLogLevel(IConfiguration configuration)
        {
            var level = configuration.GetValue<string>(LogLevelConfigurationPath);
            if (string.IsNullOrEmpty(level))
                return LogEventLevel.Warning;

            switch (level)
            {
                case "Debug": return LogEventLevel.Debug;
                case "Error": return LogEventLevel.Error;
                case "Fatal": return LogEventLevel.Fatal;
                case "Information": return LogEventLevel.Information;
                case "Verbose": return LogEventLevel.Verbose;
                default: return LogEventLevel.Warning;
            }
        }

        public static void SetSwitchToInitialLogLevel(IConfiguration configuration) =>
            Switch.MinimumLevel = GetInitialLogLevel(configuration);
    }
}
