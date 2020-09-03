using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySqlConnectionLibrary
{
    public class MySqlMain
    {
        public MySqlConnect SqlConnection = new MySqlConnect();

        public void Connect(string serverName, string databaseName, string databaseUsername, string databasePassword)
        {
            var connectionString = SqlConnection.ConnectionString(serverName, databaseName, databaseUsername, databasePassword);
            try
            {
                SqlConnection.ConnectToDatabase(connectionString);
            }
            catch
            {
                Console.WriteLine("Unable to connect to database");
            }
        }
            
        public MySqlDataReader Query(string query)
        {
            try
            {
                if (SqlConnection.CheckConnectionStatus())
                {
                    return SqlConnection.GetReader(SqlConnection.DatabaseConnection, query);
                }
                else
                {
                    Console.WriteLine("Connection status false");
                }
            }
            catch
            {
                Console.WriteLine("Invalid query");
            }
            return null;
        }
    } 
}

