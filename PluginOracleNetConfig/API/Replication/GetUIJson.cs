using System.Collections.Generic;
using Newtonsoft.Json;

namespace PluginOracleNetConfig.API.Replication
{
    public static partial class Replication
    {
        public static string GetUIJson()
        {
            Dictionary<string, object> UiJson = new Dictionary<string, object>
            {
                {"ui:order", new [] { 
                    "SchemaName",
                    "GoldenTableName",
                    "VersionTableName"
                }
                }
            };

            return JsonConvert.SerializeObject(UiJson);
        }
    }
}
