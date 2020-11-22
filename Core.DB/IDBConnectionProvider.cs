using System;
using System.Data;

namespace Core.DB
{
    public interface IDBConnectionProvider
    {
        string        ConnectionString { get; }
        void          PerformAction(Action<IDbConnection>   action);
        T             PerformFunc<T>(Func<IDbConnection, T> func);
        IDbConnection Open();
        IDbConnection Create();
    }
}
