using System.Collections.Generic;
using PluginOracleNetConfig.DataContracts;

namespace PluginOracleNetConfig.API.Utility
{
    public static class Constants
    {
        public static string ReplicationRecordId = "NaveegoReplicationRecordId";
        public static string ReplicationVersionIds = "NaveegoVersionIds";
        public static string ReplicationVersionRecordId = "NaveegoReplicationVersionRecordId";

        public static string ReplicationMetaDataTableName = "NaveegoReplicationMetaData";
        public static string ReplicationMetaDataJobId = "NaveegoJobId";
        public static string ReplicationMetaDataRequest = "Request";
        public static string ReplicationMetaDataReplicatedShapeId = "NaveegoShapeId";
        public static string ReplicationMetaDataReplicatedShapeName = "NaveegoShapeName";
        public static string ReplicationMetaDataTimestamp = "Timestamp";

        public static List<ReplicationColumn> ReplicationMetaDataColumns = new List<ReplicationColumn>
        {
            new ReplicationColumn
            {
                ColumnName = ReplicationMetaDataJobId,
                DataType = "VARCHAR2(255)",
                PrimaryKey = true
            },
            new ReplicationColumn
            {
                ColumnName = ReplicationMetaDataRequest,
                PrimaryKey = false,
                DataType = "CLOB"
            },
            new ReplicationColumn
            {
                ColumnName = ReplicationMetaDataReplicatedShapeId,
                DataType = "VARCHAR2(255)",
                PrimaryKey = false
            },
            new ReplicationColumn
            {
                ColumnName = ReplicationMetaDataReplicatedShapeName,
                DataType = "CLOB",
                PrimaryKey = false
            },
            new ReplicationColumn
            {
                ColumnName = ReplicationMetaDataTimestamp,
                DataType = "VARCHAR2(255)",
                PrimaryKey = false
            }
        };
    }
}