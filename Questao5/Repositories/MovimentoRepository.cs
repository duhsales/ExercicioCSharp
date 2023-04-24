using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using Xunit.Sdk;

namespace Questao5
{
    public class MovimentoRepository : IRepositoryMovimento<Movimento>
    {
        private readonly DatabaseConfig databaseConfig;
        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public async Task<Movimento> Add(Movimento item)
        {
            return await Task.Run(() => {

                try
                {
                    using (SqliteConnection connection = new SqliteConnection(databaseConfig.Name))
                    {
                        string sql = "INSERT INTO MOVIMENTO (DATAMOVIMENTO, IDCONTACORRENTE, IDMOVIMENTO, TIPOMOVIMENTO, VALOR) VALUES (@DataMovimento, @IdContaCorrente, @IdMovimento, @TipoMovimento, @Valor)";
                        connection.Execute(sql, item);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                return item;
            });
        }

        public async Task<string> GetSaldo(string Conta)
        {
            return await Task.Run(() => {
                try
                {
                    using (SqliteConnection connection = new SqliteConnection(databaseConfig.Name))
                    {
                        string sql = "SELECT COALESCE(SUM(CASE WHEN TIPOMOVIMENTO = 'D' THEN VALOR * (-1) ELSE VALOR END), 0) AS VALOR FROM MOVIMENTO WHERE IDCONTACORRENTE = '" + Conta + "'";
                        var resp = connection.ExecuteScalar(sql);

                        if (resp != null)
                        {
                            return resp.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                return "0.00";
            });
        }
    }
}