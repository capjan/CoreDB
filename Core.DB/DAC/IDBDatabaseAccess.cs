using System.Collections.Generic;

namespace Core.DB.DAC
{
    public interface IDBDatabaseAccess
    {
        bool                       CanCreateDatabase { get; }
        bool                       CanDeleteDatabase { get; }
        IDBDatabase                CreateDatabase(IDBDatabaseSettings settings);
        void                       DeleteDatabase(string              dbName);
        IReadOnlyList<IDBDatabase> ListDatabases();
        IDBDatabase                OpenDatabase(string dbName);
    }
}
