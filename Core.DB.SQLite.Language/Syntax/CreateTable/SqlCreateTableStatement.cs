using System.Collections.Generic;
using Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition;

namespace Core.DB.SQLite.Language.Syntax.CreateTable
{
    public class SqlCreateTableStatement: ISqlStatement
    {
        public bool                         IsTemporary  { get; set; }
        public bool                         IfNotExits   { get; set; }
        public string                       Name         { get; set; }
        public string                       SchemaName   { get; set; }
        public bool                         WithoutRowId { get; set; }
        public IList<ISqlColumnDefinition> Columns      { get; } = new List<ISqlColumnDefinition>();
    }
}