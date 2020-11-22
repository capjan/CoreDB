using System;
using System.IO;
using System.Linq;
using Core.DB.SQLite;
using Core.IO.Impl;
using Xunit;

namespace Core.DB.Test
{
    public class DBUnitTests
    {
        [Fact]
        public void TestDatabaseAccess()
        {
            var    tfp      = new DefaultTempUtil();
            var fileName = "";
            tfp.UseFile(tempFile =>
            {
                fileName = tempFile;
                var server       = SQLiteServer.FromPath(tempFile);
                var allDatabases = server.ListDatabases();
                
                Assert.StartsWith("3", server.Version);
                Assert.Single(allDatabases);
                var db       = allDatabases[0];
                var settings = db.Settings as SQLiteDatabaseSettings ?? throw new InvalidOperationException("settings must be sqlite");
                Assert.Equal("main", db.Name);
                Assert.Equal(tempFile, settings.Path);
            });
            Assert.True(!File.Exists(fileName));
        }

        [Fact]
        public void TestTAbleAccess()
        {
            var tfp      = new DefaultTempUtil();
            var fileName = "";
            tfp.UseFile(tempFile =>
            {
                fileName = tempFile;
                var server = SQLiteServer.FromPath(tempFile);
                var db     = server.OpenDatabase("main");
                var tables = db.ListTables();

            });
            Assert.True(!File.Exists(fileName));
        }
    }
}
