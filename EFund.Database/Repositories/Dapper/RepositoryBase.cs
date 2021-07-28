using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EFund.Domain.Models.Repositories.Dapper
{
    public abstract class RepositoryBase : IDisposable
    {

        public RepositoryBase(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        protected SqlConnection SqlConnection { get; }

        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
            {
                SqlConnection.Dispose();
            }

            _isDisposed = false;
        }
    }

}
