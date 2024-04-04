using DataLogicLayer;
using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

            string query = "SELECT `id`, `title`, `text` FROM `blog`";

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

                    blogs.Add(blog);
                }

                dataReader.Close();
                connection.CloseConnection();
            }

            return blogs;
        }



        //Create New Blog
        public void CreateBlog(Blog blog)
        {
            string query = "INSERT INTO blog (Title, Text) VALUES (@Title, @Text)";
            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Text", blog.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Delete blog
        public void DeleteBlogs(int id)
        {
            DatabaseConnection databaseConnection = new DatabaseConnection();

            string query = "DELETE FROM blog WHERE Id = @Id";
            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
