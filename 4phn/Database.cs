using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace _4phn
{
    class Database
    {
        private static string connectionString;

        private static void Init()
        {
            connectionString = String.Format(
                Const.SQL.CONNECTION_STRING,
                Const.SQL.DATABASE,
                Properties.Settings.Default.MYSQLServer,
                Properties.Settings.Default.MYSQLUser,
                Properties.Settings.Default.MYSQLPassword
                );
        }

        public static void execute(string sql)
        {
            Init();
            MySqlConnection myConnection = new MySqlConnection(connectionString);
            MySqlCommand myCommand = new MySqlCommand(sql, myConnection);
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        public static DataView getData(string sql)
        {
            Init();
            MySqlConnection myConnection = new MySqlConnection(Database.connectionString);
            MySqlCommand myCommand = new MySqlCommand(sql, myConnection);
            MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(myCommand);
            myConnection.Open();
            DataSet dataset = new DataSet();
            myDataAdapter.Fill(dataset);
            myConnection.Close();
            return dataset.Tables[0].AsDataView();
        }

    }
}
