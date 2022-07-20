using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PluginOracleNetConfig.Helper;

namespace PluginOracleNetConfig.API.Factory
{
    public class Connection : IConnection
    {
        private static bool _settingsApplied = false;
        private readonly OracleConnection _conn;

        public Connection(Settings settings)
        {
            if (_settingsApplied == false)
            {
                _settingsApplied = true;
            }

            _conn = new OracleConnection(settings.GetConnectionString());
        }

        public async Task OpenAsync()
        {
            await _conn.OpenAsync();
        }

        public async Task CloseAsync()
        {
            await _conn.CloseAsync();
        }

        public ITransaction BeginTransaction()
        {
            var txn = _conn.BeginTransaction();
            return new Transaction(txn);
        }

        public Task<bool> PingAsync()
        {
            return Task.FromResult(true);
        }

        public IDbConnection GetConnection()
        {
            return _conn;
        }
    }
}