using System.Data;
using System.Threading.Tasks;

namespace AlsacWebApiCore.Infrastructure
{
    public interface IConnection
    {
        string ConnectionString { set; }
        IDbConnection GetIDbConnection();
        string ServerName { get; }
        Task<bool> ConnectionIsUp();
    }
}
