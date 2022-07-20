using System.Data;
using PluginOracleNetConfig.Helper;

namespace PluginOracleNetConfig.API.Factory
{
    public interface IConnectionFactory
    {
        void Initialize(Settings settings);
        IConnection GetConnection();
        ICommand GetCommand(string commandText, IConnection conn);
    }
}