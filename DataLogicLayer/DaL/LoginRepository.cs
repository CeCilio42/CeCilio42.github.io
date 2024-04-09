using DataAccessLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DaL
{
    public class LoginRepository
    {
        public (bool, int) Login(string username, string password, int id)
        {
            string queryLogin = "SELECT id FROM users WHERE username = @Username AND password = @Password;";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryLogin, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); 
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return (true, Convert.ToInt32(result)); 
                    }
                    else
                    {
                        return (false, 0); 
                    }
                }
            }
        }


        public bool CheckRole(string username, string password)
        {
            string queryLogin = "SELECT role FROM users WHERE username = @Username AND password = @Password;";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryLogin, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); 
                    var role = cmd.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(role))
                    {
                        if (role == "admin")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false; 
                    }
                }
            }
        }

        public int CheckId(string username)
        {
            string queryLogin = "SELECT id FROM users WHERE username = @Username;";
            int id = 0; 

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryLogin, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        id = Convert.ToInt32(result); 
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }

            return id;
        }

        



    }
}
