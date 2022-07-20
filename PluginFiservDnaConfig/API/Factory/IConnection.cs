using System.Data;
using System.Threading.Tasks;

namespace PluginFiservDnaConfig.API.Factory
{
    public interface IConnection
    {
        Task OpenAsync();
        Task CloseAsync();
        ITransaction BeginTransaction();
        Task<bool> PingAsync();
        IDbConnection GetConnection();
    }
}