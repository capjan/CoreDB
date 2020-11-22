namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints
{
    public class CheckConstraint : ISqlColumnConstraint
    {
        public string Expression { get; set; }
    }
}