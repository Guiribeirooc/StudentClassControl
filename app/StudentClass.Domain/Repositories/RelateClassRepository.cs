using Dapper;
using StudentClass.Domain.Interfaces;
using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;
using StudentClassInfra.Configuration;

namespace StudentClass.Domain.Repositories
{
    public class RelateClassRepository : BaseRepository, IRelateClassRepository
    {
        public RelateClassRepository(IConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }

        public void Add(RelateClassRequest request)
        {
            var sql = @$"INSERT INTO aluno_turma 
                         VALUES (@IdStudent, @IdClass)";

            var param = new DynamicParameters();
            param.Add("@IdStudent", request.IdStudent);
            param.Add("@IdClass", request.IdClass);

            conn.Execute(sql: sql, param: param);
        }

        public void Delete(int idStudent, int idClass)
        {
            var sql = @$"DELETE FROM aluno_turma 
                         WHERE aluno_id = @IdStudent 
                         AND turma_id = @IdClass";

            var param = new DynamicParameters();
            param.Add("@IdStudent", idStudent);
            param.Add("@IdClass", idClass);

            conn.Execute(sql: sql, param: param);
        }

        public RelateClassModel? Get(int idStudent, int idClass)
        {
            var sql = @$"SELECT 
                         aluno_id AS IdStudent,
                         turma_id AS IdClass
                         FROM aluno_turma
                         WHERE aluno_id = @IdStudent 
                         AND turma_id = @IdClass";

            var param = new DynamicParameters();
            param.Add("@IdStudent", idStudent);
            param.Add("@IdClass", idClass);

            return conn.QueryFirstOrDefault<RelateClassModel>(sql: sql, param: param);
        }

        public List<RelateClassModel> GetAll()
        {
            var sql = @$"SELECT aluno_id AS IdStudent,
                         turma_id AS IdClass
                         FROM aluno_turma";

            return conn.Query<RelateClassModel>(sql: sql).ToList();
        }
    }
}
