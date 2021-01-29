using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

// Conexão com o Banco de dados
namespace Escola.API.Data.Repositories
{
    public class RepositoryBase
    {
        protected IConfiguration configuration;

        internal IDbConnection Connection
        {
            get
            {
                var connect = new NpgsqlConnection(configuration["ConnectionString"]);

                connect.Open();
                return connect;
            }
        }
    }
}
