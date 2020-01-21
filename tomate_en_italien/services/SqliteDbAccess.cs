using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using tomate_en_italien.util;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace tomate_en_italien
{
    class SqliteDbAccess
    {
        public static List<Task> LoadTask()
        {
            using(IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Task>("SELECT * FROM TASK", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveTasks(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO TASK (libelle, count) VALUES (@Libelle, @Count)", task);
            }
        }

        public static void UpdateCountTask(Task task)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE TASK SET count=count+1 WHERE libelle= @Libelle", task);
            }
        }

        private static string LoadConnectionString(string id= "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
