namespace Core.DB.SQLite.Language.Syntax.CreateTable
{
    public enum ConflictClause
    {
        Default,
        Rollback,
        Abort,
        Fail,
        Ignore,
        Replace
    }
}