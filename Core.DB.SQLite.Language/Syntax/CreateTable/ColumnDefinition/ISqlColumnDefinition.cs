using System.Collections.Generic;
using Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints;

namespace Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition
{
    public interface ISqlColumnDefinition
    {
        IList<ISqlColumnConstraint> Constraints { get; }
        string Name { get; set; }
        string Type { get; set; }
    }
}