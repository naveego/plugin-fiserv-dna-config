using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace PluginOracleNetConfig.API.Factory
{
    public class Command : ICommand
    {
        private readonly OracleCommand _cmd;

        public Command()
        {
            _cmd = new OracleCommand();
        }

        public Command(string commandText)
        {
            _cmd = new OracleCommand(commandText);
        }

        public Command(string commandText, IConnection conn)
        {
            _cmd = new OracleCommand(commandText, (OracleConnection) conn.GetConnection());
        }

        public void SetConnection(IConnection conn)
        {
            _cmd.Connection = (OracleConnection) conn.GetConnection();
        }

        public void SetCommandText(string commandText)
        {
            _cmd.CommandText = commandText;
        }

        public void AddParameter(string name, object value)
        {
            _cmd.Parameters.Add(name, value);
        }

        public async Task<IReader> ExecuteReaderAsync()
        {
            return new Reader(await _cmd.ExecuteReaderAsync());
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            return await _cmd.ExecuteNonQueryAsync();
        }
    }
}