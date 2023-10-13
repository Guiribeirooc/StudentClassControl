using StudentClass.Domain.Models;
using StudentClass.Domain.Models.Requests;

namespace StudentClass.Domain.Interfaces
{
    public interface IRelateClassRepository
    {
        RelateClassModel? Get(int idStudent, int idClass);
        List<RelateClassModel> GetAll();
        void Add(RelateClassRequest request);
        void Delete(int idStudent, int idClass);
    }
}
