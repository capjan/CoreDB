using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.SQLite.Model
{
    public class DatabaseInfo
    {
        public int    Seq  { get; set; }
        public string Name { get; set; }
        public string File { get; set; }

        public override string ToString()
        {
            return $"name: {Name}, file: {File}";
        }
    }

}
