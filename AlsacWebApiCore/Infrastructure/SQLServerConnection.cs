using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Serilog;

namespace AlsacWebApiCore.Infrastructure
{
    public class SqlServerConnection : ConnectionBase
    {
        public override IDbConnection GetIDbConnection() =>
            new SqlConnection(ConnectionString);

        public override async Task<bool> ConnectionIsUp()
        {
            try
            {
                using (var db = GetIDbConnection())
                {
                    const string sql = "SELECT 1";
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
