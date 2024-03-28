using BusinessLogicLayer.Entitys;
using MySql.Data.MySqlClient;

namespace DataLogicLayer.DaL
{
    public class CategorieDaL
    {
        DatabaseConnection connection = new DatabaseConnection();



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



    }
}
