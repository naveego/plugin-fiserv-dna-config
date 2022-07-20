using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PluginOracleNetConfig.API.Factory;
using PluginOracleNetConfig.DataContracts;

// --- Sourced from MySQL Plugin version 1.5.2 ---

namespace PluginOracleNetConfig.API.Write
{
    public static partial class Write
    {
        private const string ColSchemaName = "SCHEMA_NAME";
        private const string ColProcedureName = "PROCEDURE_NAME";
        private const string ColProcedureId = "PROCEDURE_ID";

        private static readonly string GetAllStoredProceduresQuery = $@"
select pr.owner as ""{ColSchemaName}""
        , pr.object_name as ""{ColProcedureName}""
        , pr.PROCEDURE_NAME as ""{ColProcedureId}""
from sys.all_procedures pr
where pr.owner not in ('ANONYMOUS','CTXSYS','DBSNMP','EXFSYS',
          'MDSYS', 'MGMT_VIEW','OLAPSYS','OWBSYS','ORDPLUGINS', 'ORDSYS',
          'OUTLN', 'SI_INFORMTN_SCHEMA','SYS','SYSMAN','SYSTEM', 'TSMSYS',
          'WK_TEST', 'WKSYS', 'WKPROXY','WMSYS','XDB','APEX_040000',
          'APEX_PUBLIC_USER','DIP', 'FLOWS_30000','FLOWS_FILES','MDDATA',
          'ORACLE_OCM', 'XS$NULL', 'SPATIAL_CSW_ADMIN_USR', 'LBACSYS',
          'SPATIAL_WFS_ADMIN_USR', 'PUBLIC', 'APEX_040200', 'DVSYS',
            'REMOTE_SCHEDULER_AGENT', 'GSMADMIN_INTERNAL')
        and object_type = 'PROCEDURE'
        and SUBSTR(pr.owner, 0, 5) <> 'APEX_'
order by ""{ColSchemaName}"",
        ""{ColProcedureName}""
";

        public static async Task<List<WriteStoredProcedure>> GetAllStoredProceduresAsync(IConnectionFactory connFactory)
        {
            var storedProcedures = new List<WriteStoredProcedure>();
            var conn = connFactory.GetConnection();

            try
            {
                await conn.OpenAsync();

                var cmd = connFactory.GetCommand(GetAllStoredProceduresQuery, conn);
                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var storedProcedure = new WriteStoredProcedure
                    {
                        SchemaName = reader.GetValueById(ColSchemaName).ToString(),
                        ProcedureName = reader.GetValueById(ColProcedureName).ToString(),
                        ProcedureId = reader.GetValueById(ColProcedureId).ToString()
                    };

                    storedProcedures.Add(storedProcedure);
                }

                return storedProcedures;
            }
            finally
            {
                await conn.CloseAsync();
            }
        }
    }
}