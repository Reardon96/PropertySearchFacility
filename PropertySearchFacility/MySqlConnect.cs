using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MySqlConnectionLibrary
{
    public class MySqlConnect
    {
        public MySqlConnection DatabaseConnection { get; set; }

        public string ConnectionString(string serverName, string databaseName, string databaseUsername, string databasePassword)
        {
            var connectionString =
                $"SERVER={serverName}; " +
                $"DATABASE={databaseName}; " +
                $"UID={databaseUsername}; " +
                $"PASSWORD={databasePassword}";

            return connectionString;
        }

        public void ConnectToDatabase(string connectionString)
        {
            DatabaseConnection = new MySqlConnection(connectionString);
            DatabaseConnection.Open();
        }

        public bool CheckConnectionStatus()
        {
            if (DatabaseConnection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            return false;
        }

        public static void CloseDatabaseConnection(MySqlConnection databaseConnection)
        {
            databaseConnection.Close();
        }

        public MySqlDataReader GetReader(MySqlConnection databaseConnection, string databaseQuery)
        {
            var databaseCommand = new MySqlCommand(databaseQuery, databaseConnection);
            var databaseReader = databaseCommand.ExecuteReader();
            return databaseReader;
        }
    }
}
    
