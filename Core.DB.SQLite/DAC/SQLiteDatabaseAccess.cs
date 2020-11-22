using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Core.DB.DAC;
using Core.DB.SQLite.Extensions;
using Core.DB.SQLite.Model;
using Dapper;

namespace Core.DB.SQLite.DAC
{
    internal class SQLiteDatabaseAccess : IDBDatabaseAccess
    {
        private readonly IDBConnectionProvider _connProvider;

        public SQLiteDatabaseAccess(IDBConnectionProvider connProvider)
        {
            _connProvider = connProvider;
        }

        public IReadOnlyList<IDBDatabase> ListDatabases(IDbConnection conn)
        {
            const string sql    = "PRAGMA database_list;";
            var queryResult = conn.Query<DatabaseInfo>(sql);
            var databases = queryResult.Select(i => new SQLiteDatabase(new SQLiteDatabaseSettings(i.Name, i.File),
                                                                       new SQLiteTableAccess(_connProvider, i.Name))).ToArray();
            return new ReadOnlyCollection<SQLiteDatabase>(databases);
        }

        public bool CanCreateDatabase => false;
        public bool CanDeleteDatabase => false;

        public IReadOnlyList<IDBDatabase> ListDatabases()
        {
            return _connProvider.PerformFunc(ListDatabases);
        }

        public IDBDatabase CreateDatabase(IDBDatabaseSettings settings)
        {
            throw new NotSupportedException("SQLite does not support creating databases, but you can attach databases if you require multi databases in one connection.");
        }

        public IDBDatabase OpenDatabase(string        dbName)
        {
            return ListDatabases().Single(db => db.Name == dbName);
        }

        public void DeleteDatabase(string dbName)
        {
            throw new NotSupportedException("SQLite does not support deletion of databases. Delete the whole file to wipe out the database if required.");
        }
    }
}
