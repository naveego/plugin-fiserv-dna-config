namespace PluginFiservDnaConfig.API.Factory
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}