namespace PluginOracleNetConfig.API.Factory
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}