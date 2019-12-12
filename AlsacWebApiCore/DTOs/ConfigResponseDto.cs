using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog.Events;

namespace AlsacWebApiCore.DTOs
{
    public class ConfigResponseDto
    {
        public List<string> DbServer { get; set; }
        public string AspNetVersion { get; set; }
        public string OsVersion { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LogEventLevel LogMinimumLevel { get; set; }
        public string Environment { get; set; }
        public string Additional { get; set; }
    }
}
