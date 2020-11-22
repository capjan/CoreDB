using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.SQLite.Language.Syntax
{
    public class SqlScript
    {
        public IList<ISqlStatement> Statements { get; } = new List<ISqlStatement>();
    }
}
