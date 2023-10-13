namespace StudentClassDomain.Interfaces
{
    public interface IBaseRepository
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
