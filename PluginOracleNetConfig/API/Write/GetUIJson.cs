using System.Collections.Generic;
using Newtonsoft.Json;

// --- Sourced from MySQL Plugin version 1.5.2 ---

namespace PluginOracleNetConfig.API.Write
{
    public static partial class Write
    {
        public static string GetUIJson()
        {
            var uiJsonObj = new Dictionary<string, object>
            {
                {"ui:order", new []
                {
                    "StoredProcedure",
                    "GoldenRecordIdParam"
                }}
            };
            return JsonConvert.SerializeObject(uiJsonObj);
        }
    }
}