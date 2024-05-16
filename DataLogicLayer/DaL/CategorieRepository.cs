using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using MySql.Data.MySqlClient;

namespace DataLogicLayer.DaL
{
    public class CategorieRepository : ICategoryRepository
    {
        DatabaseConnection connection = new DatabaseConnection();


        //Get all categories
        public List<CategorieDTO> SetList()
        {
            List<CategorieDTO> categories = new List<CategorieDTO>();
            string query = "SELECT `id`, `title` FROM `categorie`";

            try
            {
                if (connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection.connection);
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            CategorieDTO categorie = new CategorieDTO();
                            categorie.Id = Convert.ToInt32(dataReader["id"]);
                            categorie.Name = dataReader["title"].ToString();

                            categories.Add(categorie);
                        }
                        dataReader.Close();
                    }
                    connection.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);
            }
            
            connection.CloseConnection();
            return categories;
        }


        public void CreateCategory(CategorieDTO categorie)
        {
            string queryCreateCategory = "INSERT INTO categorie (Title) VALUES (@Title);";

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryCreateCategory, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", categorie.Name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);
            }

            connection.CloseConnection();
        }



        public void EditCategory(CategorieDTO categorie)
        {
            string queryEditCategory = "UPDATE categorie SET Title = @Title WHERE Id = @Id;";

            try
            {
                using (var connection = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi533837;Database=dbi533837;Pwd=Iqx1F6WrNs"))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(queryEditCategory, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", categorie.Name);
                        cmd.Parameters.AddWithValue("@Id", categorie.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("An error occurred: " + ex.Message);
            }

            connection.CloseConnection();
        }
    }
}
