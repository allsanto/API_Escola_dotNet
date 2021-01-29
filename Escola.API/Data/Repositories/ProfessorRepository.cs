using Dapper;
using Escola.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Escola.API.Data.Repositories
{
    public class ProfessorRepository : RepositoryBase
    {
        public ProfessorRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        public int Insert(ProfessorEntity professor)
        {
            using var db = Connection;


            var query = @"INSERT INTO Professor
                            (nome,
                            idade,
                            data_nascimento,
                            id_unidade)
                            VALUES(@Nome,
                                   @Idade,
                                   @DataNascimento,
                                   @IdUnidade)
                            RETURNING id_professor;";

            return db.ExecuteScalar<int>(query, new 
            { 
                professor.Nome,
                professor.Idade,
                professor.DataNascimento,
                professor.IdUnidade
            });
        }


        public ProfessorEntity GetProfessor(int id)
        {
            using var db = Connection;

            var query = @"SELECT id_professor,
                                 nome,
                                 idade,
                                 data_nascimento,
                                 status,
                                 id_unidade
                         FROM Professor
                            WHERE id_professor = @id;";

            return db.QueryFirstOrDefault<ProfessorEntity>(query, new { id });
        }
        public IEnumerable<ProfessorEntity> GetAllProfessor()
        {
            using var db = Connection;

            var query = @"SELECT id_professor,
                                 nome,
                                 idade,
                                 data_nascimento,
                                 status,
                                 id_unidade
                        FROM Professor
                            WHERE status = 1;";

            return db.Query<ProfessorEntity>(query);
        }
        public int GetStatusById(int idUnidade)
        {
            using var db = Connection;

            var query = @"SELECT status
                            FROM Unidade
                                WHERE id_unidade = @idUnidade;";

            return db.ExecuteScalar<int>(query, new { idUnidade });
        }


        public int Update(ProfessorEntity professor)
        {
            var db = Connection;

            var query = @"UPDATE Professor
                            SET nome = @Nome,
                                idade = @Idade,
                                data_nascimento = @DataNascimento,
                                id_unidade = @IdUnidade
                        WHERE id_professor = @IdProfessor;";
            return db.Execute(query, new { 
                professor.IdProfessor,
                professor.Nome,
                professor.Idade,
                professor.DataNascimento,
                professor.IdUnidade
            });
        }

        public int Delete(int id)
        {
            using var db = Connection;

            var query = @"UPDATE Professor
                            SET status = 2
                          WHERE id_professor = @id;";

            return db.Execute(query, new { id });
        }
    }
}
