using System.Collections.Generic;
using Core.DB.DAC;

namespace Core.DB.Impl
{
    public class DBServer : IDBServer
    {
            private readonly IDBDatabaseAccess _databaseAccess;

            public DBServer(IDBDatabaseAccess databaseAccess, string version)
            {
                _databaseAccess = databaseAccess;
                Version    = version;
            }

            public bool                       CanCreateDatabase                            => _databaseAccess.CanCreateDatabase;
            public bool                       CanDeleteDatabase                            => _databaseAccess.CanDeleteDatabase;
            public IReadOnlyList<IDBDatabase> ListDatabases()                              => _databaseAccess.ListDatabases();
            public IDBDatabase                CreateDatabase(IDBDatabaseSettings settings) => _databaseAccess.CreateDatabase(settings);
            public IDBDatabase                OpenDatabase(string                dbName)   => _databaseAccess.OpenDatabase(dbName);
            public void                       DeleteDatabase(string              dbName)   => _databaseAccess.DeleteDatabase(dbName);
            public string                     Version { get; }
    }
}
