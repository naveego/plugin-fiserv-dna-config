using System.Data;
using PluginFiservDnaConfig.Helper;

namespace PluginFiservDnaConfig.API.Factory
{
    public interface IConnectionFactory
    {
        void Initialize(Settings settings);
        IConnection GetConnection();
        ICommand GetCommand(string commandText, IConnection conn);
    }
}