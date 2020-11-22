namespace Core.DB.SQLite
{
    public class SQLiteTable : IDBTable
    {
        public string Type     { get; }
        public string Name     { get; }
        public string Tbl_Name { get; }
        public string RootPage { get; }
        public string Sql      { get; }

        public SQLiteTable(string name, string type, string rootPage, string tblName, string sql)
        {
            Type     = type;
            RootPage = rootPage;
            Tbl_Name = tblName;
            Sql = sql;
            Name     = name;
        }

    }
}
