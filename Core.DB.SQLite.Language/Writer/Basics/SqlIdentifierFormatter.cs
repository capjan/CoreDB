using System;
using System.IO;
using Core.Text.Formatter;

namespace Core.DB.SQLite.Language.Writer
{
    public class SqlIdentifierFormatter : ITextFormatter<string>
    {
        public           SqlIdentifierStyle Style { get; }
        private readonly string             _quoteStart;
        private readonly string             _quoteEnd;

        public SqlIdentifierFormatter(SqlIdentifierStyle style)
        {
            Style = style;
            switch (style)
            {
                case SqlIdentifierStyle.Default:
                    _quoteStart = "\"";
                    _quoteEnd   = "\"";
                    break;
                case SqlIdentifierStyle.Microsoft:
                    _quoteStart = "[";
                    _quoteEnd   = "]";
                    break;
                case SqlIdentifierStyle.MySQL:
                    _quoteStart = "`";
                    _quoteEnd   = "`";
                    break;
                default:
                    throw new NotSupportedException("unknown SqlIdentifierStyle");
            }
        }
        
        public void Write(string value, TextWriter writer)
        {
            writer.Write(_quoteStart);
            writer.Write(value);
            writer.Write(_quoteEnd);
        }
    }
}