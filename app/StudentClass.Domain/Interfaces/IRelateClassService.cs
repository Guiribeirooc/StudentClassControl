using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;

namespace StudentClass.Domain.Interfaces
{
    public interface IRelateClassService
    {
        RequestResult Get(int idStudent, int idClass);
        RequestResult GetAll();
        RequestResult Add(RelateClassRequest request);
        RequestResult Delete(int idStudent, int idClass);
    }
}
