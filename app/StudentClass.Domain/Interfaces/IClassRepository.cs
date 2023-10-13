using StudentClass.Domain.Models;

namespace StudentClass.Domain.Interfaces
{
    public interface IClassRepository
    {
        ClassModel? Get(int id);
        List<ClassModel> GetAll();
        void Add(ClassModel classModel);
        void Update(ClassModel classModel);
        void Delete(int id);
    }
}
