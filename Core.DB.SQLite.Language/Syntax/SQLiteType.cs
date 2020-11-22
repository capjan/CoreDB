using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.SQLite.Language.Syntax
{
    public class SQLiteStorageClass
    {
        public const string Null    = "NULL";
        public const string Integer = "INTEGER";
        public const string Real    = "REAL";
        public const string Text    = "TEXT";
        public const string Blob    = "BLOB";
    }

    public class SQLiteTypeAffinity
    {
        public const string Text    = "TEXT";
        public const string Numeric = "NUMERIC";
        public const string Integer = "INTEGER";
        public const string Real    = "REAL";
        public const string Blob    = "BLOB";
    }
}
