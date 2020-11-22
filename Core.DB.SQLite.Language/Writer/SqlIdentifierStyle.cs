namespace Core.DB.SQLite.Language.Writer
{
    public enum SqlIdentifierStyle
    {
        /// <summary>
        /// Default SQL Identifier Style via double quotes. Used by SQLite, Postgres
        /// </summary>
        Default,
        /// <summary>
        /// Microsoft Style used in T-SQL for the products SQL-Server and Access.
        /// </summary>
        Microsoft,
        /// <summary>
        /// Quotation Style used by MySQL Databases.
        /// </summary>
        MySQL
    }
}