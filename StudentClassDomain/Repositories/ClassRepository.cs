using Dapper;
using StudentClassDomain.Interfaces;
using StudentClassDomain.Models;
using StudentClassInfra.Configuration;

namespace StudentClassDomain.Repositories
{
    public class ClassRepository : BaseRepository, IClassRepository
    {
        public ClassRepository(IConnectionConfig connectionConfig) : base(connectionConfig) 
        { 
        }

        public void Add(ClassModel classModel)
        {
            var sql = @$"INSERT INTO turma
                         VALUES (@IdCurso, @Turma, @Ano)";

            var param = new DynamicParameters();
            param.Add("@Turma", classModel.Turma);
            param.Add("@IdCurso", classModel.IdCurso);
            param.Add("@Ano", classModel.Ano);

            conn.Execute(sql: sql, param: param);
        }

        public void Delete(int id)
        {
            var sql = @$"DELETE FROM turma
                         WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            conn.Execute(sql: sql, param: param);
        }

        public ClassModel? Get(int id)
        {
            var sql = @$"SELECT id AS Id, 
                        curso_id AS IdCurso, 
                        turma AS Turma, 
                        ano AS Ano
                        FROM turma
                        WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            return conn.QueryFirstOrDefault<ClassModel>(sql:sql, param: param);
        }

        public List<ClassModel> GetAll()
        {
            var sql = @$"SELECT id AS Id, 
                        curso_id AS IdCurso, 
                        turma AS Turma, 
                        ano AS Ano
                        FROM turma";

            return conn.Query<ClassModel>(sql: sql).ToList();
        }

        public void Update(ClassModel classModel)
        {
            var sql = @$"   UPDATE turma 
                            SET curso_id = ISNULL(@IdCurso,curso_id), 
                                turma = ISNULL(@Turma,turma), 
                                ano = ISNULL(@Ano,ano) 
                                WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Turma", classModel.Turma);
            param.Add("@IdCurso", classModel.IdCurso);
            param.Add("@Ano", classModel.Ano);
            param.Add("@Id", classModel.Id);

            conn.Execute(sql: sql, param: param);
        }
    }
}
