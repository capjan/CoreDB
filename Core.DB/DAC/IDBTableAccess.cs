using System.Collections.Generic;

namespace Core.DB.DAC
{
    public interface  IDBTableAccess
    {
        IReadOnlyList<IDBTable> ListTables();
        void                CreateTable(IDBTable table);
    }
}
