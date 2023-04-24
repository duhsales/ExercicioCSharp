using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            string _connectionString = _configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite");

            return new SqliteConnection(_connectionString);
        }
    }
}
