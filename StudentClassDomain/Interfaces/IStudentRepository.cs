using StudentClassDomain.Models;

namespace StudentClassDomain.Interfaces
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
