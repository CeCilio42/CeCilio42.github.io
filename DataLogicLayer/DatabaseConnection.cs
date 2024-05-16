using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer
{
    public class DatabaseConnection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        
        public DatabaseConnection()
        {
            Initialize();
        }

      
        private void Initialize()
        {
            string connectionString;
            connectionString = $"SERVER=studmysql01.fhict.local;DATABASE=dbi533837;UID=dbi533837;PASSWORD=Iqx1F6WrNs;";

            connection = new MySqlConnection(connectionString);
        }

        // Open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
              
                return false;
            }
        }

        // Close connection to database
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                
                return false;
            }
        }
    }
}
