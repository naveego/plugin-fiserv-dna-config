using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using PluginOracleNetConfig.API.Factory;
using PluginOracleNetConfig.API.Utility;
using PluginOracleNetConfig.DataContracts;
using PluginOracleNetConfig.Helper;

namespace PluginOracleNetConfig.API.Discover
{
    public static partial class Discover
    {
        private static async Task<Schema> GetRefreshSchemaForQuery(IConnectionFactory connFactory, ConfigQuery query)
        {
            // read in schema from settings
            var conn = connFactory.GetConnection();

            try
            {
                // open connection
                await conn.OpenAsync();
                
                // re-create schema
                var outputSchema = new Schema
                {
                    Id = query.Id,
                    Query = query.Query,
                    Name = query.Id,
                    Description = ""
                };
                
                // run the SELECT query from the import schema
                var cmd = connFactory.GetCommand(query.Query, conn);
                var reader = await cmd.ExecuteReaderAsync();
                var schemaTable = reader.GetSchemaTable();
                
                var properties = new List<Property>();
                        
                var unNamedColIndex = 0;

                // get each column and create a property for the column
                foreach (DataRow row in schemaTable.Rows)
                {
                    // get the column name
                    var colName = row["ColumnName"].ToString();
                    if (string.IsNullOrWhiteSpace(colName))
                    {
                        colName = $"UNKNOWN_{unNamedColIndex}";
                        unNamedColIndex++;
                    }

                    // create property
                    var property = new Property
                    {
                        Id = Utility.Utility.GetSafeName(colName, '"'),
                        Name = colName,
                        Description = "",
                        Type = GetPropertyType(row),
                        IsKey = string.IsNullOrWhiteSpace(row["IsKey"].ToString()) ? false : Boolean.Parse(row["IsKey"].ToString()),
                        IsNullable = string.IsNullOrWhiteSpace(row["AllowDBNull"].ToString()) ? false : Boolean.Parse(row["AllowDBNull"].ToString()),
                        IsCreateCounter = false,
                        IsUpdateCounter = false,
                        PublisherMetaJson = ""
                    };

                    // add property to properties
                    properties.Add(property);
                }

                // add only discovered properties to schema
                outputSchema.Properties.Clear();
                outputSchema.Properties.AddRange(properties);
                
                outputSchema.Query = query.Query; // include query in the description

                return outputSchema;
            }
            finally
            {
                await conn.CloseAsync();
            }    
        }
    }
}