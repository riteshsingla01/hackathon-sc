using AlsacWebApiCore.DTOs;
using Microsoft.Extensions.Configuration;

namespace ConstituentSearch.Delegates
{
    public class Version
    {
        public static VersionResponseDto GetVersion(IConfiguration configuration) =>
            new VersionResponseDto
            {
                VersionNumber = configuration.GetValue<string>("versionInfo:versionNumber") + "!",
                BuildDate = configuration.GetValue<string>("versionInfo:buildDate")
            };
    }
}
