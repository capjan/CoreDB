using Core.DB.SQLite.Language.Syntax.Enums;

namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints
{
    public class PrimaryKeyConstraint : ISqlColumnConstraint
    {
        public SortOrder          Order          { get; set; } = SortOrder.Default;
        public bool           Autoincrement  { get; set; } = false;
        public ConflictClause ConflictClause { get; set; } = ConflictClause.Default;
    }
}