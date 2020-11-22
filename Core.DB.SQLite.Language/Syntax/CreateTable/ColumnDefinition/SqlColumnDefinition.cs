using System.Collections.Generic;
using Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints;

namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition
{
    public class SqlColumnDefinition : ISqlColumnDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<ISqlColumnConstraint> Constraints { get; } = new List<ISqlColumnConstraint>();
    }
}