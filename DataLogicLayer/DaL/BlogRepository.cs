using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BlogRepository : IBlogRepository
    {
        DatabaseConnection connection = new DatabaseConnection();



        //Get All Blogs
        public List<BlogDTO> GetBlogs()
        {
            List<BlogDTO> blogs = new List<BlogDTO>();
            string query = "SELECT b.id, b.title, b.text, c.title AS categorie_title " +
                           "FROM blog b " +
                           "LEFT JOIN blogcategorie bc ON b.id = bc.blog_id " +
                           "LEFT JOIN categorie c ON bc.categorie_id = c.id " +
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

                        blogs.Add(blogDTO);
                    }

                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.CloseConnection();
            }

            return blogs;
        }




        //Create New Blog
        public void CreateBlog(BlogDTO blog, int categoryId)
        {
            int blogId;
            string queryBlog = "INSERT INTO blog (Title, Text) VALUES (@Title, @Text); SELECT LAST_INSERT_ID();";
            string queryConnectBlogCat = "INSERT INTO blogcategorie (blog_id, categorie_id) VALUES (@BlogID, @CategoryID);";

            try
            {
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryBlog, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", blog.Name);
                        cmd.Parameters.AddWithValue("@Text", blog.Description);
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
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
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
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
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
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
                using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
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
            catch (MySqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            connection.CloseConnection();
        }



    }
}
