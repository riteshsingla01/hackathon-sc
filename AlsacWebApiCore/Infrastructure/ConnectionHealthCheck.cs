using System.Collections.Generic;
using System.Threading.Tasks;
using AlsacWebApiCore.DTOs;

namespace AlsacWebApiCore.Infrastructure
{
    public class ConnectionHealthCheck
    {
        public static async Task<List<SubsystemStatusResponseItem>>CheckConnections(IConnectionFactory connectionFactory)
        {
            var statusList = new List<SubsystemStatusResponseItem>();

            foreach ((string key, IConnection value) in connectionFactory.GetConnectionProviders())
            {
                var status = new SubsystemStatusResponseItem { SubsystemName = "Connection " + key, SubsystemUp = await value.ConnectionIsUp() };
                statusList.Add(status);
            }

            return statusList;
        }
    }
}
