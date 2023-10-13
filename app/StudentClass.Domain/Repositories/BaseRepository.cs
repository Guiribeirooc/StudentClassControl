using StudentClassDomain.Interfaces;
using StudentClassInfra.Configuration;
using System.Data.SqlClient;

namespace StudentClass.Domain.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected SqlTransaction transaction;
        protected SqlConnection conn;
        protected string Connection { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BaseRepository(IConnectionConfig connectionConfig)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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