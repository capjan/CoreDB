using System.IO;
using Core.DB.SQLite.Language.Syntax;
using Core.Text.Formatter;

namespace Core.DB.SQLite.Language.Writer.Statements.Comment
{
    public class SQLCommentWriter : ITextFormatter<SqlCommentStatement>
    {
        public void Write(SqlCommentStatement value, TextWriter writer)
        {
            writer.Write($"-- {value.Text}");
        }
    }
}