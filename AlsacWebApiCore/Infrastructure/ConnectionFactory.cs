using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace AlsacWebApiCore.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly Dictionary<string, IConnection> _connectionProviders = new Dictionary<string, IConnection>();
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            BuildConnectionProviders();
        }

        public IDbConnection Fetch(string dbName) =>
            _connectionProviders.GetValueOrDefault(dbName, null).GetIDbConnection();


        public List<string> GetDbServerNames() =>
            _connectionProviders.Select(c => c.Value.ServerName).ToList();

        public Dictionary<string, IConnection> GetConnectionProviders()
        {
            return _connectionProviders;
        }

        private void BuildConnectionProviders()
        {
            var section = _configuration.GetSection("ConnectionStrings");
            foreach (IConfigurationSection s in section.GetChildren())
            {
                if (s.GetChildren().Any())
                {
                    var children = s.GetChildren() as IConfigurationSection[] ?? s.GetChildren().ToArray();
                    var provider = children.FirstOrDefault(k => k.Key == "provider")?.Value;
                    var connectionString = children.FirstOrDefault(k => k.Key == "connection")?.Value;
                    if (provider != null && provider.Equals("oracle"))
                        _connectionProviders.Add(s.Key, new OracleServerConnection() { ConnectionString = AppendUsernamePassword(connectionString, GetUsernamePassword(s.Key)) });
                    else
                        _connectionProviders.Add(s.Key, new SqlServerConnection() { ConnectionString = AppendUsernamePassword(connectionString, GetUsernamePassword(s.Key)) });
                }
                else
                    _connectionProviders.Add(s.Key, new SqlServerConnection() { ConnectionString = AppendUsernamePassword(s.Value, GetUsernamePassword(s.Key)) });
            }
        }

        private UsernamePassword GetUsernamePassword(string key)
        {
            var secret = _configuration.GetSection("ConnectionStringSecrets").GetSection(key);
            return new UsernamePassword(secret.GetValue<string>("username"), secret.GetValue<string>("password"));
        }

        private string AppendUsernamePassword(string connectionString, UsernamePassword usernamePassword)
        {
            connectionString = connectionString.Trim();
            if (!connectionString.EndsWith(';'))
                connectionString += ";";
            return connectionString + $"User Id={usernamePassword.Username};Password={usernamePassword.Password}";
        }

        private struct UsernamePassword
        {
            public readonly string Username;
            public readonly string Password;

            public UsernamePassword(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }
    }
}
