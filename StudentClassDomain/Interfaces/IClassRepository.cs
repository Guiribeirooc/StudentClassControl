using StudentClassDomain.Models;

namespace StudentClassDomain.Interfaces
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
