namespace Core.DB.SQLite.Model
{
    public class TableInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Tbl_Name { get; set; }
        public string RootPage { get; set; }
        public string Sql { get; set; }

        public override string ToString()
        {
            return $"name: {Name}, Tbl_Name: {Tbl_Name}";
        }
    }
}