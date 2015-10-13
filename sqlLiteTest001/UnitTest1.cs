using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sqlLiteTest001
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestConnection()
    {
      /*
      DbProviderFactory fact = DbProviderFactories.GetFactory("System.Data.SQLite");
      using (DbConnection cnn = fact.CreateConnection())
      {
        cnn.ConnectionString = "Data Source=test.db3";
        cnn.Open();
      }
      */
      var builder = new SQLiteConnectionStringBuilder();
      builder.DataSource = "test.db";
      SQLiteConnection sql = new SQLiteConnection(builder.ToString());
      sql.Open();
      sql.Close();
      sql.Dispose();
    }

    [TestMethod]
    public void TestQuery()
    {
      var builder = new SQLiteConnectionStringBuilder();
      builder.DataSource = "test.db";
      using (DbConnection connection = new SQLiteConnection(builder.ToString()))
      {
        connection.Open();
        using (var cmd1 = connection.CreateCommand())
        {
          cmd1.CommandText = @"SELECT name FROM sqlite_master WHERE type='table' AND name='table_test';";
          var reader = cmd1.ExecuteReader();
          if (reader.Read())
          {
            var tableName = reader.GetString(0);
            System.Diagnostics.Trace.WriteLine(String.Format("table name={0}", tableName));
          }
          else
          {
            using (var cmd2 = connection.CreateCommand())
            {
              cmd2.CommandText = @"Create Table 'table_test' (num Integer, str)";
              cmd2.ExecuteNonQuery();
            }
          }
        }
      }
    }
  }
}
