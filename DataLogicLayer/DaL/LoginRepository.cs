﻿using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.Errors;
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
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
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
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred in the repository: " + ex.Message);

                return (false, 0, null);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred in the repository: " + ex.Message);

                return (false, 0, null);
            }
        }



        public bool CheckRole(string username, string password)
        {
            string queryLogin = "SELECT role FROM users WHERE username = @Username AND password = @Password;";

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
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
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred in the repository: " + ex.Message);

                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred in the repository: " + ex.Message);

                return false;
            }
        }

    }
}
