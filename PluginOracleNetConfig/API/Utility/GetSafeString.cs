namespace PluginOracleNetConfig.API.Utility
{
    public static partial class Utility
    {
        public static string GetSafeString(string unsafeString, string escapeChar = "'", string newChar = "''")
        {
            return unsafeString.Replace(escapeChar, newChar);
        }
    }
}