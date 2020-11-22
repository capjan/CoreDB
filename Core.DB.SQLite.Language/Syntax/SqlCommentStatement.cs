using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.SQLite.Language.Syntax
{
    public class SqlCommentStatement: ISqlStatement
    {
        public string Text { get; set; }
    }
}
