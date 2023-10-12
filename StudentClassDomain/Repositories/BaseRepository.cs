using StudentClassDomain.Interfaces;
using StudentClassInfra.Configuration;
using System.Data.SqlClient;

namespace StudentClassDomain.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected SqlTransaction transaction;
        protected SqlConnection conn;
        protected string Connection { get; }

        public BaseRepository(IConnectionConfig connectionConfig)
        {
            Connection = connectionConfig.GetConnectionString();
            conn = new SqlConnection(Connection);
        }

        public void BeginTransaction()
        {
            OpenConnection();
            transaction = conn.BeginTransaction();
        }

        private void OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            conn.Open();
        }

        public void Commit()
        {
            transaction.Commit();
            CloseConnection();
        }

        private void CloseConnection()
        {
            conn.Close();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (transaction != null)
                    transaction.Dispose();

                conn.Close();
                conn.Dispose();
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
            CloseConnection();
        }
    }
}