using System;

namespace PluginOracleNetConfig.API.Utility
{
    public static partial class Utility
    {
        public static string GetTableNameFromId(string id)
        {
            // remove quotations
            var nameStr = id.Replace("\"", "");

            if (!id.Contains("."))
            {
                return nameStr;
            }
            
            // get substr after "."
            var dotPos = nameStr.IndexOf(".", StringComparison.CurrentCulture);
            return nameStr.Substring(dotPos+1);
        }
    }
}