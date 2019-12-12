using System.Collections.Generic;
using System.Data;

namespace AlsacWebApiCore.Infrastructure
{
    public interface IConnectionFactory 
    {
        IDbConnection Fetch(string dbName);
        List<string> GetDbServerNames();
        Dictionary<string, IConnection> GetConnectionProviders();
    }
}
