using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Naveego.Sdk.Plugins;
using PluginOracleNetConfig.API.Factory;
using PluginOracleNetConfig.API.Utility;
using PluginOracleNetConfig.DataContracts;
using PluginOracleNetConfig.Helper;

namespace PluginOracleNetConfig.API.Discover
{
    public static partial class Discover
    { 
        public static async IAsyncEnumerable<Schema> GetAllSchemas(IConnectionFactory connFactory, Settings settings, int sampleSize = 5)
        {
            // get the config list
            List<ConfigQuery> configQueries = new List<ConfigQuery>();
            List<Schema> resultSchemas = new List<Schema>();

            if (settings != null)
            {
                // load config queries from file provided in settings
                configQueries = Utility.Utility.GetConfigQueries(true, settings.ConfigSchemaFilePath);
            }
            else
            {
                // load cached config queries
                configQueries = Utility.Utility.GetConfigQueries();
            }
            
            // loop over each config list item
            foreach (var cq in configQueries)
            {
                // synthesize schema properties from the query
                // add schema to a list
                resultSchemas.Add(await GetRefreshSchemaForQuery(connFactory, cq));
            }
            
            // loop over final list
            foreach (var rs in resultSchemas)
            {
                if (rs != null)
                {
                    // for each schema, add sample and count
                    yield return await AddSampleAndCount(connFactory, rs, sampleSize);
                }
            }
        }

        private static async Task<Schema> AddSampleAndCount(IConnectionFactory connFactory, Schema schema,
            int sampleSize)
        {
            // add sample and count
            var records = Read.Read.ReadRecords(connFactory, schema).Take(sampleSize);
            schema.Sample.AddRange(await records.ToListAsync());
            schema.Count = await GetCountOfRecords(connFactory, schema);

            return schema;
        }
        
        public static PropertyType GetType(string dataType, object dataLength = null, object dataPrecision = null, object dataScale = null)
        {
            switch (dataType)
            {
                case "DATE":
                case var t when t != null && t.Contains("TIMESTAMP"):
                    return PropertyType.Datetime;
                case "TIME":
                    return PropertyType.Time;
                case "NUMBER":
                    if (dataScale != null && dataPrecision != null)
                    {
                        if ((int)dataScale == 0 || (int)dataScale == -127)
                        {
                            if ((int)dataPrecision <= 16)
                            {
                                return PropertyType.Integer;
                            }
                        }
                    }

                    return PropertyType.Decimal;
                case "FLOAT":
                case "BINARY_FLOAT":
                case "DOUBLE":
                case "BINARY_DOUBLE":
                    return PropertyType.Float;
                case "DECIMAL":
                case "BIGINT":
                    return PropertyType.Decimal;
                case "BOOLEAN":
                    return PropertyType.Bool;
                case "BLOB":
                    return PropertyType.Blob;
                case "XMLTYPE":
                    return PropertyType.Xml;
                case "CLOB":
                case "NCLOB":
                    return PropertyType.Text;
                case "CHAR":
                case "VARCHAR":
                case "NCHAR":
                case "NVARCHAR":
                case "VARCHAR2":
                case "NVARCHAR2":
                    if (dataLength != null)
                    {
                        if ((int)dataLength >= 1024)
                        {
                            return PropertyType.Text;
                        }
                    }
                    return PropertyType.String;
                default:
                    return PropertyType.String;
            }
        }
        
        private static string GetTypeAtSource(string dataType, object dataLength, object dataPrecision, object dataScale)
        {
            switch (dataType)
            {
                case "CHAR":
                case "VARCHAR2":
                case "NCHAR":
                case "NVARCHAR2":
                    if (dataLength != DBNull.Value)
                    {
                        return $"{dataType}({dataLength})";
                    }
                    break;
                case "NUMBER":
                    if (dataPrecision != DBNull.Value && dataScale != DBNull.Value)
                    {
                        return $"{dataType}({dataPrecision},{dataScale})";
                    }
                    break;
            }

            return dataType;
        }

        public static Schema.Types.DataFlowDirection GetDataFlowDirection(string direction)
        {
            switch (direction)
            {
                case var t when t.Contains("write") && t.Contains("read"):
                    return Schema.Types.DataFlowDirection.ReadWrite;
                case var t when t.Contains("write"):
                    return Schema.Types.DataFlowDirection.Write;
                default:
                    return Schema.Types.DataFlowDirection.Read;
            }
        }
    }
}