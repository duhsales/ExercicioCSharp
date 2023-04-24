using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using System.Data;
using System.Data.Common;
using Xunit.Sdk;

namespace Questao5
{
    public class ContaCorrenteRepository : IRepositoryContaCorrente<ContaCorrente>
    {

        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrente> Add(ContaCorrente item)
        {
            return await Task.Run(() => {

                try
                {
                    using (SqliteConnection connection = new SqliteConnection(databaseConfig.Name))
                    {
                        string sql = "INSERT INTO CONTACORRENTE (IDCONTACORRENTE, NUMERO, NOME, ATIVO) VALUES (@IdContaCorrente, @Numero, @Nome, @Ativo)";
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

        public async Task<ContaCorrente> GetById(string Conta)
        {
            ContaCorrente contaCorrente = new ContaCorrente();
            return await Task.Run(() => {
                try
                {
                    using (SqliteConnection connection = new SqliteConnection(databaseConfig.Name))
                    {
                        string sql = "SELECT * FROM CONTACORRENTE WHERE IDCONTACORRENTE = '" + Conta + "'";
                        contaCorrente = connection.QueryFirst<ContaCorrente>(sql);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                return contaCorrente;
            });
        }
    }
}