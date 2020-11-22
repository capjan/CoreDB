using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.DB.DAC;
using Core.DB.SQLite.Model;
using Dapper;

namespace Core.DB.SQLite.DAC
{
    public class SQLiteTableAccess: IDBTableAccess
    {
        private readonly IDBConnectionProvider _connProvider;
        private readonly string                _databaseName;

        public SQLiteTableAccess(IDBConnectionProvider connProvider, string databaseName)
        {
            _connProvider = connProvider;
            _databaseName = databaseName;
        }

        public IReadOnlyList<IDBTable> ListTables()
        {
            var sql = $"SELECT * FROM [{_databaseName}].sqlite_master WHERE type='table' AND NOT name = 'sqlite_sequence'";
            return _connProvider.PerformFunc<IReadOnlyList<IDBTable>>(conn =>
            {
                return new ReadOnlyCollection<SQLiteTable>(conn.Query<TableInfo>(sql).Select(i => new SQLiteTable(i.Name, i.Type, i.RootPage, i.Tbl_Name, i.Sql)).ToArray());
            });
            
        }

        public void CreateTable(IDBTable table)
        {
            throw new System.NotImplementedException();
        }
    }
}
