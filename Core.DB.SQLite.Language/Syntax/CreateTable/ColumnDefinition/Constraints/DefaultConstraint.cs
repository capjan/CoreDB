namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints
{
    public class DefaultConstraint : ISqlColumnConstraint
    {
        public string Expression { get; set; }
    }
}