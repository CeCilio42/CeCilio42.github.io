using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entitys;
using DataLogicLayer.Entitys;
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
        public (bool, int, string) Login(string username, string password)
        {
            string queryLogin = "SELECT id, profile_picture FROM users WHERE username = @Username AND password = @Password;";
            try
            {
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryLogin, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                int userId = Convert.ToInt32(dataReader["id"]);
                                string profilePictureUrl = dataReader["profile_picture"].ToString();

                                return (true, userId, profilePictureUrl);
                            }
                            else
                            {
                                return (false, 0, null); 
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
                return (false, 0, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return (false, 0, null);
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
