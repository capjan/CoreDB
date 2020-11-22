using System;
using System.Collections.Generic;
using Core.DB.DAC;

namespace Core.DB
{
    public interface IDBServer: IDBDatabaseAccess
    {
        string Version { get; }
    }
}
