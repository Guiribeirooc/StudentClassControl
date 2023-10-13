using Dapper;
using StudentClassDomain.Interfaces;
using StudentClassDomain.Models;
using StudentClassDomain.Models.Requests;
using StudentClassInfra.Configuration;

namespace StudentClassDomain.Repositories
{
    public class RelateClassRepository : BaseRepository, IRelateClassRepository
    {
        public RelateClassRepository(IConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }

        public void Add(RelateClassRequest request)
        {
            var sql = @$"INSERT INTO aluno_turma 
                         VALUES (@IdAluno, @IdTurma)";

            var param = new DynamicParameters();
            param.Add("@IdAluno", request.IdAluno);
            param.Add("@IdTurma", request.IdTurma);

            conn.Execute(sql: sql, param: param);
        }

        public void Delete(int idStudent, int idClass)
        {
            var sql = @$"DELETE FROM aluno_turma 
                         WHERE aluno_id = @IdAluno 
                         AND turma_id = @IdTurma";

            var param = new DynamicParameters();
            param.Add("@IdAluno", idStudent);
            param.Add("@IdTurma", idClass);

            conn.Execute(sql: sql, param: param);
        }

        public RelateClassModel? Get(int idStudent, int idClass)
        {
            var sql = @$"SELECT 
                         aluno_id AS IdAluno,
                         turma_id AS IdTurma
                         FROM aluno_turma
                         WHERE aluno_id = @IdAluno 
                         AND turma_id = @IdTurma ";

            var param = new DynamicParameters();
            param.Add("@IdAluno", idStudent);
            param.Add("@IdTurma", idClass);

            return conn.QueryFirstOrDefault<RelateClassModel>(sql: sql, param: param);
        }

        public List<RelateClassModel> GetAll()
        {
            var sql = @$"SELECT * FROM aluno_turma";
            return conn.Query<RelateClassModel>(sql: sql).ToList();
        }
    }
}
