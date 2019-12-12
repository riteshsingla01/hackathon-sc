using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Serilog;

namespace AlsacWebApiCore.Infrastructure
{
    public class OracleServerConnection : ConnectionBase
    { 
        public override IDbConnection GetIDbConnection() =>
            new OracleConnection(ConnectionString);

        public override async Task<bool> ConnectionIsUp()
        {
            try
            {
                using (var db = GetIDbConnection())
                {
                    const string sql = "SELECT 1 FROM DUAL";
                    var result = await db.QueryAsync(sql);
                    return result.Count() == 1;
                }
            }
            catch (Exception e)
            {
                Log.Error($"Error checking Oracle Database Connection Health {e}");
                throw;
            }
        }
    }
}
