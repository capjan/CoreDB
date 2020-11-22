namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints
{
    public class NotNullConstraint : ISqlColumnConstraint
    {
        public ConflictClause OnConflict { get; set; } = ConflictClause.Default;
    }
}