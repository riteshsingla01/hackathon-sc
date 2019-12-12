using AlsacWebApiCore.Controllers;
using AlsacWebApiCore.DTOs;
using AlsacWebApiCore.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace ConstituentSearch.Delegates
{
    public class Config
    {
        public static ConfigResponseDto GetConfig(IConnectionFactory connectionFactory, IHostingEnvironment env)
        {
            var response = ConfigController.GetConfigDefault(connectionFactory, env);
            response.Additional = "Some additional stuff";
            return response;
        }
    }
}
