using System;
using System.Data;

namespace Core.DB.Impl
{
    public class DBConnectionProvider: IDBConnectionProvider
    {
        public           string              ConnectionString { get; }
        private readonly Func<IDbConnection> _buildNewConnFunc;

        public DBConnectionProvider(string connectionString, Func<IDbConnection> connBuilder)
        {
            ConnectionString  = connectionString;
            _buildNewConnFunc = connBuilder;
        }

        public void PerformAction(Action<IDbConnection> action)
        {
            using (var conn = Open())
                action(conn);
        }

        public T PerformFunc<T>(Func<IDbConnection, T> func)
        {
            var conn = Open();
            var result = func(conn);
            conn.Dispose();
            return result;
        }

        public IDbConnection Open()
        {
            var conn = Create();
            conn.Open();
            return conn;
        }

        public IDbConnection Create()
        {
            return _buildNewConnFunc();
        }
    }
}
