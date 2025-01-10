using System.Data;
using Npgsql;
namespace Sistema_CursosOnline.Data
{
    public class PostgresConnection
    {
        private readonly string _connectionString;

        public PostgresConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
