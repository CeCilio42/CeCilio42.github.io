using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogicLayer.DaL
{
    public class BlogDaL
    {
        DatabaseConnection connection = new DatabaseConnection();

        public List<Blog> SetBlog()
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



 
    }
}
