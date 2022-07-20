using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace PluginOracleNetConfig.API.Factory
{
    public class Reader : IReader
    {
        private readonly OracleDataReader _reader;

        public Reader(IDataReader reader)
        {
            _reader = (OracleDataReader) reader;
        }

        public async Task<bool> ReadAsync()
        {
            return await _reader.ReadAsync();
        }

        public async Task CloseAsync()
        {
            await _reader.CloseAsync();
        }

        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public object GetValueById(string id, char trimChar = '"')
        {
            return _reader[id.Trim(trimChar)];
        }

        public bool HasRows()
        {
            return _reader.HasRows;
        }
    }
}