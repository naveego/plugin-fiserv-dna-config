using Naveego.Sdk.Plugins;
using PluginOracleNetConfig.API.Utility;
using PluginOracleNetConfig.DataContracts;

namespace PluginOracleNetConfig.API.Replication
{
    public static partial class Replication
    {
        public static ReplicationTable GetVersionReplicationTable(Schema schema, string safeSchemaName, string safeVersionTableName)
        {
            var versionTable = ConvertSchemaToReplicationTable(schema, safeSchemaName, safeVersionTableName);
            versionTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationVersionRecordId,
                DataType = "VARCHAR(255)",
                PrimaryKey = true
            });
            versionTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationRecordId,
                DataType = "VARCHAR(255)",
                PrimaryKey = false
            });

            return versionTable;
        }
    }
}
