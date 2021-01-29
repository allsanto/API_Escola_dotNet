using Dapper;
using Escola.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Escola.API.Data.Repositories
{
    // Métodos para manipular dados
    public class AlunoRepository : RepositoryBase
    {
        public AlunoRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        public int Insert(AlunoEntity aluno)
        {
            using var db = Connection;

            var query = @"INSERT INTO ALUNO
                            (nome, 
                             idade, 
                             data_nascimento, 
                             id_unidade) 
                            VALUES(@Nome, 
                                   @Idade, 
                                   @DataNascimento, 
                                   @IdUnidade)
                            RETURNING id_aluno;";
            return db.ExecuteScalar<int>(query, new
            {
                aluno.Nome,
                aluno.Idade,
                aluno.DataNascimento,
                aluno.IdUnidade
            });
        }
        
        public int Update(AlunoEntity aluno)
        {
            using var db = Connection;

            var query = @"UPDATE Aluno
                            SET nome = @Nome,
                                idade = @Idade,
                                data_nascimento = @DataNascimento,
                                id_unidade = @IdUnidade
                          WHERE id_aluno = @IdAluno;";
            return db.Execute(query, new
            {
                aluno.IdAluno,
                aluno.Nome,
                aluno.Idade,
                aluno.DataNascimento,
                aluno.IdUnidade
            });
        }

        public int Delete(int id)
        {
            using var db = Connection;

            var query = @"UPDATE Aluno
                            SET status = 2
                          WHERE id_aluno = @id;";

            return db.Execute(query, new { id });
        }

        // Criar metodos para fazer o get de um aluno no banco
        public AlunoEntity GetAluno(int id)
        {
            using var db = Connection;

            var query = @"SELECT id_aluno,
                                 nome,
                                 idade,
                                 data_nascimento,
                                 status,
                                 id_unidade
                          FROM Aluno
                            WHERE id_aluno = @id;";

            // QueryFirstOrDefault - Para retornar a primeira entidade que achar ou null
            return db.QueryFirstOrDefault<AlunoEntity>(query, new { id });
        }

        public int GetStatusById(int idUnidade)
        {
            using var db = Connection;

            var query = @"SELECT status 
                            FROM Unidade 
                                WHERE id_unidade = @idUnidade";

            return db.ExecuteScalar<int>(query, new { idUnidade });
        }

        public IEnumerable<AlunoEntity> GetAllAlunos()
        {
            using var db = Connection;

            var query = @"SELECT id_aluno,
                                 nome,
                                 idade,
                                 data_nascimento,
                                 status,
                                 id_unidade
                          FROM Aluno
                            WHERE status = 1;";

            return db.Query<AlunoEntity>(query);
        }
    }
}
