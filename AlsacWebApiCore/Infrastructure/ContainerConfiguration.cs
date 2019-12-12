using Microsoft.Extensions.Configuration;

namespace AlsacWebApiCore.Infrastructure
{
    public class ContainerConfiguration
    {
        public const string AppConfigPath = "/app-config/appsettings.json";
        public const string AppSecretsPath = "/app-sec/appsettings.secrets.json";

        public static IConfigurationRoot GetContainerConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile(AppConfigPath, optional: true)
                .AddJsonFile(AppSecretsPath, optional: true)
                .Build();
    }
}
