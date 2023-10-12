using StudentClassDomain.Models;
using StudentClassDomain.Models.Requests;

namespace StudentClassDomain.Interfaces
{
    public interface IRelateClassService
    {
        RequestResult Get(int idStudent, int idClass);
        RequestResult GetAll();
        RequestResult Add(RelateClassRequest request);
        RequestResult Delete(int idStudent, int idClass);
    }
}
