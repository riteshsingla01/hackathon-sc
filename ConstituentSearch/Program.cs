using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using AlsacWebApiCore.Infrastructure;

namespace ConstituentSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(ContainerConfiguration.GetContainerConfiguration())
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
