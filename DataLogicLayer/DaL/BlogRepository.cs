using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Errors;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entitys;
using DataLogicLayer;
using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogicLayer
{
    public class BlogRepository : IBlogRepository
    {
        DatabaseConnection connection = new DatabaseConnection();



        //Get All Blogs
        public List<BlogDTO> GetBlogs()
        {
            List<BlogDTO> blogs = new List<BlogDTO>();
            string query = "SELECT b.id, b.title, b.text, b.date, u.username, u.profile_picture, c.title " +
                            "AS categorie_title FROM blog b " +
                            "LEFT JOIN users u ON b.user_id = u.id " +
                            "LEFT JOIN blogcategorie bc " +
                            "ON b.id = bc.blog_id " +
                            "LEFT JOIN categorie c " +
                            "ON bc.categorie_id = c.id " +
                            "ORDER BY b.id DESC;";
            try
            {
                if (connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        BlogDTO blogDTO = new BlogDTO();
                        blogDTO.id = Convert.ToInt32(dataReader["id"]);
                        blogDTO.Name = dataReader["title"].ToString();
                        blogDTO.Description = dataReader["text"].ToString();
                        blogDTO.CategoryTitle = dataReader["categorie_title"].ToString();
                        blogDTO.Date = dataReader["date"].ToString();
                        blogDTO.Username = dataReader["username"].ToString();
                        blogDTO.profile_picture = dataReader["profile_picture"].ToString();

                        blogs.Add(blogDTO);
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                //new EventHandler(, ex);
                Console.WriteLine("An error occured: " + ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return blogs;
        }


        public List<BlogDTO> SearchBlogsByInput(string input)
        {

            List<BlogDTO> blogs = new List<BlogDTO>();
            string query = "SELECT b.id, b.title, b.text, b.date, u.username, u.profile_picture, c.title " +
                           "AS categorie_title FROM blog b " +
                           "LEFT JOIN users u ON b.user_id = u.id " +
                           "LEFT JOIN blogcategorie bc ON b.id = bc.blog_id " +
                           "LEFT JOIN categorie c ON bc.categorie_id = c.id " +
                           "WHERE b.title LIKE @input OR b.text LIKE @input " +
                           "ORDER BY b.id DESC";


            try
            {
                if (connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                    cmd.Parameters.AddWithValue("@input", "%" + input + "%");
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        BlogDTO blogDTO = new BlogDTO();
                        blogDTO.id = Convert.ToInt32(dataReader["id"]);
                        blogDTO.Name = dataReader["title"].ToString();
                        blogDTO.Description = dataReader["text"].ToString();
                        blogDTO.CategoryTitle = dataReader["categorie_title"].ToString();
                        blogDTO.Date = dataReader["date"].ToString();
                        blogDTO.Username = dataReader["username"].ToString();
                        blogDTO.profile_picture = dataReader["profile_picture"].ToString();

                        blogs.Add(blogDTO);
                    }

                    dataReader.Close();
                }

                if (blogs.Count == 0)
                {
                    throw new NoBlogsFoundException("No blogs found for the given input.");
                }

                return blogs;
            }
            catch (NoBlogsFoundException ex)
            {
                throw;
            }
            finally
            {
                connection.CloseConnection();
            }

        }




        //Create New Blog
        public void CreateBlog(BlogDTO blog, int categoryId, int? user_id)
        {
            int blogId;
            string queryBlog = "INSERT INTO blog (title, text, date, user_id) VALUES (@Title, @Text, CURDATE(), @user_id); SELECT LAST_INSERT_ID()";
            string queryConnectBlogCat = "INSERT INTO blogcategorie (blog_id, categorie_id) VALUES (@BlogID, @CategoryID);";

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryBlog, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", blog.Name);
                        cmd.Parameters.AddWithValue("@Text", blog.Description);
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        blogId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (var cmd = new MySqlCommand(queryConnectBlogCat, connection))
                    {
                        cmd.Parameters.AddWithValue("@BlogID", blogId);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);

            }

            connection.CloseConnection();
        }



        //Delete blog
        public void DeleteBlogs(int blogId)
        {
            string queryDeleteBlogCategorie = "DELETE FROM blogcategorie WHERE blog_id = @BlogID;";
            string queryDeleteBlog = "DELETE FROM blog WHERE id = @BlogID;";

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryDeleteBlogCategorie, connection))
                    {
                        cmd.Parameters.AddWithValue("@BlogID", blogId);
                        cmd.ExecuteNonQuery();
                    }
                    using (var cmd = new MySqlCommand(queryDeleteBlog, connection))
                    {
                        cmd.Parameters.AddWithValue("@BlogID", blogId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.CloseConnection();
            }
        }



        public BlogDTO GetBlogById(int id)
        {
            string query = "SELECT `id`, `title`, `text` FROM `blog` WHERE id = @id";
            BlogDTO blog = new BlogDTO();

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                blog.id = Convert.ToInt32(dataReader["id"]);
                                blog.Name = dataReader["title"].ToString();
                                blog.Description = dataReader["text"].ToString();
                            }
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            finally
            {
               connection.CloseConnection();
            }

            return blog;
        }



        public void EditBlog(BlogDTO blog)
        {
            string query = "UPDATE blog SET Title = @Title, Text = @Text WHERE Id = @BlogId;";
            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@BlogId", blog.id);
                        cmd.Parameters.AddWithValue("@Title", blog.Name);
                        cmd.Parameters.AddWithValue("@Text", blog.Description);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }

            connection.CloseConnection();
        }


        public List<BlogDTO> GetUserBlogs(int? userId)
        {
            List<BlogDTO> blogs = new List<BlogDTO>();

            string query = "SELECT b.id, b.title, b.text, u.profile_picture, c.title " +
                           "AS categorie_title FROM blog b " +
                           "LEFT JOIN users u ON b.user_id = u.id " +
                           "LEFT JOIN blogcategorie bc ON b.id = bc.blog_id " +
                           "LEFT JOIN categorie c ON bc.categorie_id = c.id " +
                           "WHERE b.user_id = @userId " +
                           "ORDER BY b.id DESC";

            try
            {
                if (connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                    // Use parameterized queries to prevent SQL injection
                    cmd.Parameters.AddWithValue("@userId", userId);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        BlogDTO blogDTO = new BlogDTO();
                        blogDTO.id = Convert.ToInt32(dataReader["id"]);
                        blogDTO.Name = dataReader["title"].ToString();
                        blogDTO.Description = dataReader["text"].ToString();
                        blogDTO.CategoryTitle = dataReader["categorie_title"].ToString();
                        blogDTO.profile_picture = dataReader["profile_picture"].ToString();

                        blogs.Add(blogDTO);
                    }

                    dataReader.Close();
                }
            }
            catch (QueryDatabaseException ex)
            {
                throw new QueryDatabaseException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.CloseConnection();
            }

            return blogs;
        }

    }
}
