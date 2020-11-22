using System.IO;

namespace Core.DB.SQLite
{
    public class SQLiteDatabaseSettings : IDBDatabaseSettings
    {
        public SQLiteDatabaseSettings(string name, string path)
        {
            Path = path;
            Name = name;
        }

        public string Path { get; }
        public string Name { get; }
    }
}