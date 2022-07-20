using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PluginOracleNetConfig.DataContracts;
using Serilog.Core;

namespace PluginOracleNetConfig.API.Utility
{
    public static partial class Utility
    {
        private static List<ConfigQuery> _configQueries = new List<ConfigQuery>();
        private static string _configSchemaFilePath = "";
    
        public static void LoadQueryConfigsFromJson(string filePath)
        {
            _configSchemaFilePath = filePath;
            
            // parse json schemas from file
            var obj = JsonConvert.DeserializeObject<List<ConfigQuery>>(File.ReadAllText(filePath));
            
            // validate schemas before sending
            var unNamedSchemas = 0;
            foreach (var configSchema in obj)
            {
                // check if id is null
                if (string.IsNullOrWhiteSpace(configSchema.Id))
                {
                    configSchema.Id = $"UnnamedSchema_{unNamedSchemas}";
                    unNamedSchemas += 1;
                }
            }

            _configQueries = obj;
        }

        public static List<ConfigQuery> GetConfigQueries(bool refresh = false, string filePath = "")
        {
            // if refresh or first time loading
            if (refresh || _configQueries.Count == 0)
            {
                LoadQueryConfigsFromJson(_configSchemaFilePath);
            }
            
            return _configQueries;
        }
        
        public static ConfigQuery GetConfigQuery(string queryId, bool refresh = false, string filePath = "")
        {
            // load a specific schema config
            return GetConfigQueries(refresh, filePath).Find(s => s.Id.Trim() == queryId.Trim());
        }
    }
}