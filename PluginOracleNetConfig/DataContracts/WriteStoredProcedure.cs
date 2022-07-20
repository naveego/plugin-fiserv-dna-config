using PluginOracleNetConfig.API.Utility;

namespace PluginOracleNetConfig.DataContracts
{
    public class WriteStoredProcedure
    {
        public string SchemaName { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureId { get; set; }

        public string GetId()
        {
            return $"{Utility.GetSafeName(SchemaName.ToAllCaps())}.{Utility.GetSafeName(ProcedureId)}";
        }
        
        public string GetFullName()
        {
            return $"{Utility.GetSafeName(SchemaName.ToAllCaps())}.{Utility.GetSafeName(ProcedureName)}";
        }
        
        public string GetName()
        {
            return Utility.GetSafeName(ProcedureName);
        }
    }
}