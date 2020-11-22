using System.Collections.Generic;
using Core.DB.DAC;
using Core.DB.SQLite.DAC;

namespace Core.DB.SQLite
{
    internal class SQLiteDatabase : IDBDatabase
    {
        private readonly SQLiteDatabaseSettings _settings;
        private readonly SQLiteTableAccess      _tableAccess;

        public SQLiteDatabase(SQLiteDatabaseSettings settings, SQLiteTableAccess tableAccess)
        {
            _settings    = settings;
            _tableAccess = tableAccess;
        }

        public string                   Name                        => _settings.Name;
        IDBDatabaseSettings IDBDatabase.Settings                    => _settings;
        public IReadOnlyList<IDBTable>  ListTables()                => _tableAccess.ListTables();
        public void                     CreateTable(IDBTable table) => _tableAccess.CreateTable(table);

        public override string ToString()
        {
            return $"Database {{ Name: {_settings.Name}, Path: {_settings.Path} }}";
        }
    }
}