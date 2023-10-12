using Dapper;
using StudentClassDomain.Interfaces;
using StudentClassDomain.Models;
using StudentClassInfra.Configuration;

namespace StudentClassDomain.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IConnectionConfig connectionConfig) : base(connectionConfig)
        {
        }
        public void Add(StudentModel student)
        {
            var sql = @$"INSERT INTO aluno 
                         VALUES (@Nome, @Usuario, @Senha)";

            var param = new DynamicParameters();
            param.Add("@Nome", student.Nome);
            param.Add("@Usuario", student.Usuario);
            param.Add("@Senha", student.Senha);

            conn.Execute(sql: sql, param: param);
        }

        public void Delete(int id)
        {
            var sql = @$"DELETE FROM aluno 
                         WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            conn.Execute(sql: sql, param: param);
        }

        public StudentModel? Get(int id)
        {
            var sql = @$"SELECT nome as Nome,
                                usuario as Usuario,
                                senha as Senha,
                         FROM aluno
                         WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            return conn.QueryFirstOrDefault<StudentModel>(sql: sql, param: param);
        }

        public List<StudentModel> GetAll()
        {
            var sql = @$"SELECT nome as Nome,
                                usuario as Usuario,
                                senha as Senha,
                         FROM aluno";
            return conn.Query<StudentModel>(sql: sql).ToList();
        }

        public void Update(StudentModel student)
        {
            var sql = @$"   UPDATE aluno 
                            SET nome = ISNULL(@Nome,nome), 
                                usuario = ISNULL(@Usuario,usuario), 
                                senha = ISNULL(@Senha,senha), 
                            WHERE id = @Id";

            var param = new DynamicParameters();
            param.Add("@Nome", student.Nome);
            param.Add("@Usuario", student.Usuario);
            param.Add("@Senha", student.Senha);
            param.Add("@Id", student.Id);

            conn.Execute(sql: sql, param: param);
        }
    }
}
