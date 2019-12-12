using System.Linq;
using System.Threading.Tasks;
using AlsacWebApiCore.DTOs;
using AlsacWebApiCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ConstituentSearch.Delegates
{
    public class Health
    {
        public static async Task<SubsystemStatusResponseDto> GetHealth(IConnectionFactory connectionFactory, IConfiguration configuration)
        {
            var statusList = await ConnectionHealthCheck.CheckConnections(connectionFactory);

            if (statusList.Count == 0)
                statusList.Add(new SubsystemStatusResponseItem { SubsystemName = "No Subsystems", SubsystemUp = true });

            return new SubsystemStatusResponseDto() { Subsystems = statusList, OverallSystemUp = statusList.All(x => x.SubsystemUp) };
        }
    }
}
