using BusinessLogicLayer.Entitys;
using DataAccessLayer.Entitys;
using DataLogicLayer;
using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BlogRepository
    {
        DatabaseConnection connection = new DatabaseConnection();



        //Get All Blogs
        public List<Blog> GetBlogs()
        {
            List<Blog> blogs = new List<Blog>();
            
            string query = "SELECT b.id, b.title, b.text, c.title AS categorie_title\r\nFROM blog b\r\nLEFT JOIN blogcategorie bc ON b.id = bc.blog_id\r\nLEFT JOIN categorie c ON bc.categorie_id = c.id\r\nORDER BY b.id DESC;";
            if (connection.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Blog blog = new Blog();
                    blog.Id = Convert.ToInt32(dataReader["id"]);
                    blog.Title = dataReader["title"].ToString();
                    blog.Text = dataReader["text"].ToString();
                    blog.CategoryTitle = dataReader["categorie_title"].ToString();


                    //blog.user = new User
                    //{
                    //    Id = Convert.ToInt32(dataReader["user_id"]),
                    //    User_Username = dataReader["username"].ToString()
                    //};

                    blogs.Add(blog);
                }

                dataReader.Close();
                connection.CloseConnection();
            }

            return blogs;
        }



        //Create New Blog
        public void CreateBlog(Blog blog, int categoryId)
        {
            int blogId;
            string queryBlog = "INSERT INTO blog (Title, Text) VALUES (@Title, @Text); SELECT LAST_INSERT_ID();";

            string queryConnectBlogCat = "INSERT INTO blogcategorie (blog_id, categorie_id) VALUES (@BlogID, @CategoryID);";
            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryBlog, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Text", blog.Text);
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



        //Delete blog
        public void DeleteBlogs(int blogId)
        {
            string queryDeleteBlogCategorie = "DELETE FROM blogcategorie WHERE blog_id = @BlogID;";
            string queryDeleteBlog = "DELETE FROM blog WHERE id = @BlogID;";

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


        public Blog GetSelectedBlogToEdit(int id)
        {
            string query = "SELECT `id`, `title`, `text` FROM `blog` WHERE id = @id";
            Blog blog = new Blog();

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        
                        blog.Id = Convert.ToInt32(dataReader["id"]);
                        blog.Title = dataReader["title"].ToString();
                        blog.Text = dataReader["text"].ToString();


                    }
                    dataReader.Close();
                }

                connection.Close();
            }

            return blog;
        }


        public void EditBlog(Blog blog)
        {
            string query = "UPDATE blog SET Title = @Title, Text = @Text WHERE Id = @BlogId;";
            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BlogId", blog.Id);
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Text", blog.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool IsBlogUserCoupled(int blogId, int userId)
        {
            string query = "SELECT COUNT(1) FROM blog_user WHERE blog_id = @BlogId AND user_id = @UserId;";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BlogId", blogId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
        }

        public bool IsBlogOwnedByUser(int blogId, int userId)
        {
            return IsBlogUserCoupled(blogId, userId);
        }

    }
}
