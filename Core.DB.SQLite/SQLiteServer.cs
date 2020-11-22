using System.Data.SQLite;
using Core.DB.Impl;
using Core.DB.SQLite.DAC;
using Dapper;

namespace Core.DB.SQLite
{
    public class SQLiteServer
    {
        public static IDBServer FromConnectionString(string connectionString)
        {
            var connProvider = new DBConnectionProvider(connectionString, () => new SQLiteConnection(connectionString));
            var dbAccess = new SQLiteDatabaseAccess(connProvider);
            var version =
                connProvider.PerformFunc(conn => conn.ExecuteScalar<string>("SELECT sqlite_version();"));
            return new DBServer(dbAccess, version);
        }

        public static IDBServer FromPath(string path)
        {
            var csb = new SQLiteConnectionStringBuilder {DataSource = path};
            return FromConnectionString(csb.ConnectionString);
        }
    }
}
