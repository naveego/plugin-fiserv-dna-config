using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Google.Protobuf.Collections;
using Naveego.Sdk.Plugins;
using PluginOracleNetConfig.API.Factory;
using PluginOracleNetConfig.Helper;

namespace PluginOracleNetConfig.API.Discover
{
    public static partial class Discover
    {
        public static async IAsyncEnumerable<Schema> GetRefreshSchemas(IConnectionFactory connFactory,
            Settings settings, RepeatedField<Schema> refreshSchemas, int sampleSize = 5)
        {
            foreach (var schema in refreshSchemas)
            {
                // refresh config queries from config file
                var importQuery = Utility.Utility.GetConfigQuery(schema.Id, true, settings.ConfigSchemaFilePath);
                var outputSchema = await GetRefreshSchemaForQuery(connFactory, importQuery);

                // get sample and count
                yield return await AddSampleAndCount(connFactory, outputSchema, sampleSize);
            }
        }
    }
}