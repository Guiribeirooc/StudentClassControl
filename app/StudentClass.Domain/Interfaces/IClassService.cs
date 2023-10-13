using StudentClass.Domain.Models;

namespace StudentClass.Domain.Interfaces
{
    public interface IClassService
    {
        RequestResult Get(int id);
        RequestResult GetAll();
        RequestResult Add(ClassModel classModel);
        RequestResult Update(ClassModel classModel);
        RequestResult Delete(int id);
    }
}
