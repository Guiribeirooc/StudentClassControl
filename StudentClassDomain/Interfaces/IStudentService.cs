using StudentClassDomain.Models;

namespace StudentClassDomain.Interfaces
{
    public interface IStudentService
    {
        RequestResult Get(int id);
        RequestResult GetAll();
        RequestResult Add(StudentModel studentModel);
        RequestResult Update(StudentModel studentModel);
        RequestResult Delete(int id);
    }
}
