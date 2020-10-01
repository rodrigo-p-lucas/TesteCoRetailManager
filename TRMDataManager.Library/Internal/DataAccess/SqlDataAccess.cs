using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool IsClosed = false;
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public IList<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IList<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void StartTransaction(string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            IsClosed = false;
        }

        public IList<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            IList<T> rows = _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            IsClosed = true;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            IsClosed = true;
        }

        public void Dispose()
        {
            if (IsClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch { }
            }

            _transaction = null;
            _connection = null;
        }
    }
}
