using Dapper;
using Escola.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Escola.API.Data.Repositories
{
    public class UnidadeRepository : RepositoryBase
    {
        public UnidadeRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        #region Método POST - Inserir Unidade - Create
        public int Insert(UnidadeEntity unidade)
        {
            using var db = Connection;
            var query = @"INSERT INTO unidade
                            (nome,
                             endereco)
                            VALUES( @Nome,
                                    @Endereco)
                           RETURNING id_unidade;";
            return db.ExecuteScalar<int>(query, new
            { 
                unidade.Nome,
                unidade.Endereco
            });
        }
        #endregion

        #region Método GET - Buscar por Unidade - Read
        public UnidadeEntity GetUnidade(int id)
        {
            using var db = Connection;
            var query = @"SELECT nome,
                                endereco,
                                id_unidade
                          FROM Unidade
                            WHERE id_unidade = @id;";
            return db.QueryFirstOrDefault<UnidadeEntity>(query, new { id });
        }

        public IEnumerable<UnidadeEntity> GetAllUnidades()
        {
            using var db = Connection;

            var query = @"SELECT id_unidade,
                                 nome,
                                 endereco,
                                 status
                         FROM Unidade
                            WHERE status = 1;";

            return db.Query<UnidadeEntity>(query);
        }
        #endregion

        #region Método UPDATE - Atualizar Unidade - Update
        public int Update(UnidadeEntity unidade)
        {
            using var db = Connection;

            var query = @"UPDATE Unidade
                            SET nome = @Nome,
                                endereco = @Endereco
                            WHERE id_unidade = @IdUnidade;";
            return db.Execute(query, new 
            { 
                unidade.Nome,
                unidade.Endereco,
                unidade.IdUnidade
            });
        }
        #endregion

        #region Método UPDATE - Atualizar Unidade - Update
        public int Delete(int id)
            {
                using var db = Connection;

                var query = @"UPDATE Unidade
                                SET status = 2
                              WHERE id_unidade = @id;";
                return db.Execute(query, new { id });   
        }
        #endregion
    }
}
