using BusinessLogicLayer.Entitys;
using MySql.Data.MySqlClient;

namespace DataLogicLayer.DaL
{
    public class CategorieRepository
    {
        DatabaseConnection connection = new DatabaseConnection();


        //Get all categories
        public List<Categorie> SetList()
        {

            List<Categorie> categories = new List<Categorie>();
            string query = "SELECT `id`, `title` FROM `categorie`";

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Categorie categorie = new Categorie();
                    categorie.Id = Convert.ToInt32(dataReader["id"]);
                    categorie.Title = dataReader["title"].ToString();

                    categories.Add(categorie);
                }

                dataReader.Close();
                connection.CloseConnection();
            }

            return categories;
        }

        public void CreateCategory(Categorie categorie)
        {
            string queryCreateCategory = "INSERT INTO categorie (Title) VALUES (@Title);";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryCreateCategory, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", categorie.Title);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void EditCategory(Categorie categorie)
        {
            string queryEditCategory = "UPDATE categorie SET Title = @Title WHERE Id = @Id;";

            using (var connection = new MySqlConnection("SERVER=127.0.0.1;DATABASE=blog database;UID=root;PASSWORD="))
            {
                connection.Open();
                using (var cmd = new MySqlCommand(queryEditCategory, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", categorie.Title);
                    cmd.Parameters.AddWithValue("@Id", categorie.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
