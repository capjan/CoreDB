using System;
using System.Collections.Generic;
using System.IO;
using Core.DB.SQLite.Language.Syntax;
using Core.DB.SQLite.Language.Syntax.CreateTable;
using Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition;
using Core.DB.SQLite.Language.Syntax.CreateTable.ColumnDefinition.Constraints;
using Core.DB.SQLite.Language.Syntax.Enums;
using Core.DB.SQLite.Language.Writer;
using Core.DB.SQLite.Language.Writer.Statements.Comment;
using Core.Text.Formatter;

namespace Core.DB.SQLite.Language.Writer
{
    public class SqlWriterSettings
    {
        public ITextFormatter<SqlCommentStatement>     CommentFormatter;
        public ITextFormatter<SqlCreateTableStatement> CreateTableFormatter;

        public SqlWriterSettings(
            ITextFormatter<SqlCommentStatement> commentWriter = default, 
            ITextFormatter<SqlCreateTableStatement> createTableWriter = default)
        {
            CommentFormatter         = commentWriter ?? new SQLCommentWriter();
            CreateTableFormatter     = createTableWriter ?? CreateDefaultIdentifierFormatter();
        }

        private static SqlCreateTableStatementFormatter CreateDefaultIdentifierFormatter()
        {
            var delimitedIdentifierFormatter = new SqlIdentifierFormatter(SqlIdentifierStyle.Default);
            var conflictClauseFormatter = new ConflictClauseFormatter();
            var constraintFormatter = new ColumnConstraintsFormatter(conflictClauseFormatter);
            var columnDefinitionFormatter    = new SqlColumnDefinitionsFormatter(delimitedIdentifierFormatter,constraintFormatter);
            return new SqlCreateTableStatementFormatter(delimitedIdentifierFormatter, columnDefinitionFormatter);
        }
    }
    public class SqlWriter: ITextFormatter<SqlScript>
    {
        private readonly SqlWriterSettings _settings;
        
        public SqlWriter(SqlWriterSettings settings = default)
        {
            _settings = settings;
        }

        public void Write(SqlScript value, TextWriter writer)
        {
            foreach (var statement in value.Statements)
            {
                if (statement is SqlCommentStatement commentStatement)
                    _settings.CommentFormatter.Write(commentStatement, writer);
                else if (statement is SqlCreateTableStatement createTableStatement) 
                    _settings.CreateTableFormatter.Write(createTableStatement, writer);
                else
                    throw new NotSupportedException("The script contains an unexpected sql statement.");
            }
        }

    }

    public class SqlCreateTableStatementFormatter : ITextFormatter<SqlCreateTableStatement>
    {
        private readonly ITextFormatter<string>                      _identifierFormatter;
        private readonly ITextFormatter<IList<ISqlColumnDefinition>> _columnDefinitionFormatter;

        public SqlCreateTableStatementFormatter(
            ITextFormatter<string> identifierFormatter, 
            ITextFormatter<IList<ISqlColumnDefinition>> columnDefinitionFormatter)
        {
            _identifierFormatter            = identifierFormatter;
            _columnDefinitionFormatter = columnDefinitionFormatter;
        }

        public void Write(SqlCreateTableStatement value, TextWriter writer)
        {
            writer.Write("CREATE ");
            if (value.IsTemporary) writer.Write("TEMPORARY ");
            writer.Write("TABLE ");
            if (value.IfNotExits) writer.Write("IF NOT EXISTS ");
            if (!string.IsNullOrWhiteSpace(value.SchemaName))
            {
                _identifierFormatter.Write(value.SchemaName, writer);
                writer.Write(".");
            }
            _identifierFormatter.Write(value.Name, writer);
            writer.WriteLine();
            
            // Column definitions
            writer.WriteLine("(");
            _columnDefinitionFormatter.Write(value.Columns, writer);
            writer.Write(")");
            
            // Without RowID
            if (value.WithoutRowId) writer.Write(" WITHOUT ROWID");
            writer.WriteLine(";");

        }
    }

    public class SqlColumnDefinitionsFormatter : ITextFormatter<IList<ISqlColumnDefinition>>
    {
        private readonly string                                      _indent;
        private readonly ITextFormatter<string>                      _identifierFormatter;
        private readonly ITextFormatter<IList<ISqlColumnConstraint>> _constraintFormatter;

        public SqlColumnDefinitionsFormatter(
            ITextFormatter<string> identifierFormatter, 
            ITextFormatter<IList<ISqlColumnConstraint>> constraintFormatter, 
            string indent = "   ")
        {
            _identifierFormatter      = identifierFormatter;
            _constraintFormatter = constraintFormatter;
            _indent                   = indent;
        }

        public void Write(IList<ISqlColumnDefinition> value, TextWriter writer)
        {
            for (var index = 0; index < value.Count; index++)
            {
                var colDef = value[index];
                writer.Write(_indent);
                _identifierFormatter.Write(colDef.Name, writer);
                writer.Write(" ");
                writer.Write(colDef.Type);
                if (colDef.Constraints.Count != 0)
                {
                    writer.Write(" ");
                    _constraintFormatter.Write(colDef.Constraints, writer);
                }
                if (index < (value.Count-1)) writer.WriteLine(",");
            }
        }
    }

    public class ConflictClauseFormatter : ITextFormatter<ConflictClause>
    {
        public void Write(ConflictClause value, TextWriter writer)
        {
            if (value == ConflictClause.Default) return;
            
            writer.Write("ON CONFLICT ");
                switch (value)
                {
                    case ConflictClause.Rollback:
                        writer.Write("ROLLBACK ");
                        break;
                    case ConflictClause.Abort: 
                        writer.Write("ABORT ");
                        break;
                    case ConflictClause.Fail:
                        writer.Write("FAIL ");
                        break;
                    case ConflictClause.Ignore:
                        writer.Write("IGNORE ");
                        break;
                    case ConflictClause.Replace:
                        writer.Write("REPLACE ");
                        break;
                    default:
                        throw new NotSupportedException($"unexpected {nameof(ConflictClause)} \"{value}\" error");
                }
            }
      
        }
    }

    public class ColumnConstraintsFormatter : ITextFormatter<IList<ISqlColumnConstraint>>
    {
        private readonly ConflictClauseFormatter _optionalConflictClauseFormatter;
        public ColumnConstraintsFormatter(ConflictClauseFormatter optionalConflictClauseFormatter)
        {
            _optionalConflictClauseFormatter = optionalConflictClauseFormatter;
        }

        public void Write(IList<ISqlColumnConstraint> value, TextWriter writer)
        {
            for (var index = 0; index < value.Count; index++)
            {
                var constraint = value[index];
                if (constraint is PrimaryKeyConstraint primaryKeyConstraint)
                {
                    writer.Write("PRIMARY KEY ");
                    switch (primaryKeyConstraint.Order)
                    {
                        case SortOrder.Default:
                            break;
                        case SortOrder.Asc:
                            writer.Write("ASC ");
                            break;
                        case SortOrder.Desc:
                            writer.Write("DESC ");
                            break;
                        default:
                            throw new NotSupportedException("Unexpected sort order detected.");
                    }
                    _optionalConflictClauseFormatter.Write(primaryKeyConstraint.ConflictClause, writer);
                    if (primaryKeyConstraint.Autoincrement)
                        writer.Write("AUTOINCREMENT ");
                }

                
            }
        }
    }
