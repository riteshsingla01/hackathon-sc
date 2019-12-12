using System.Data;
using System.Threading.Tasks;

namespace AlsacWebApiCore.Infrastructure
{
    public abstract class ConnectionBase : IConnection
    {
        private string _connectionString = "Value Not Initialized";

        public string ConnectionString
        {
            protected get => _connectionString;
            set
            {
                _connectionString = value;
                ServerName = _connectionString.Substring(0, _connectionString.IndexOf(';'));
            }
        }

        public abstract IDbConnection GetIDbConnection();

        public string ServerName { get; protected set; }

        public abstract Task<bool> ConnectionIsUp();
    }
}
