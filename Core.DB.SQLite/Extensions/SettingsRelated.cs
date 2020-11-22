using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.SQLite.Extensions
{
    internal static class SettingsRelated
    {
        public static SQLiteDatabaseSettings ToSQLite(this IDBDatabaseSettings settings)
        {
            if (!(settings is SQLiteDatabaseSettings concreteSettings))
                throw new ArgumentException("SQLite implementation requires a SQLite Database configuration",
                                            nameof(settings));
            return concreteSettings;
        }

    }
}
