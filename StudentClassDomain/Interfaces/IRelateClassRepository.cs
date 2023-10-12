using StudentClassDomain.Models;
using StudentClassDomain.Models.Requests;

namespace StudentClassDomain.Interfaces
{
    public interface IRelateClassRepository
    {
        RelateClassModel? Get(int idStudent, int idClass);
        List<RelateClassModel> GetAll();
        void Add(RelateClassRequest request);
        void Delete(int idStudent, int idClass);
    }
}
