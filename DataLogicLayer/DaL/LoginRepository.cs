using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DaL
{
    public class LoginRepository : ILoginRepository
    {
        public (bool, int) Login(string username, string password, int id)
        {
            string queryLogin = "SELECT id FROM users WHERE username = @Username AND password = @Password;";
            try
            {
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
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return (false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return (false, 0);
            }
        }



        public bool CheckRole(string username, string password)
        {
            string queryLogin = "SELECT role FROM users WHERE username = @Username AND password = @Password;";

            try
            {
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
                            return role == "admin";
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }



    }
}
