using Core.DB.DAC;

namespace Core.DB
{
    public interface IDBDatabase: IDBTableAccess
    {
        /// <summary>
        /// Returns the Name of the Database
        /// </summary>
        string              Name     { get; }

        /// <summary>
        /// Additional Settings of the Database
        /// </summary>
        IDBDatabaseSettings Settings { get; }
    }
}
