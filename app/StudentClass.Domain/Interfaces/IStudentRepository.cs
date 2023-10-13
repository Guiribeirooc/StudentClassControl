using StudentClass.Domain.Models;

namespace StudentClass.Domain.Interfaces
{
    public interface IStudentRepository
    {
        StudentModel? Get(int id);
        List<StudentModel> GetAll();
        void Add(StudentModel student);
        void Update(StudentModel student);
        void Delete(int id);
    }
}
