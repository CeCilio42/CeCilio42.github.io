using System.Diagnostics;
using System.Collections.Generic;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.DaL;

namespace BusinessLogicLayer.Classes
{
    public class CategorieService
    {
        //CategorieViewModel categorieModel = new CategorieViewModel();

        List<Categorie> categories = new List<Categorie>();
        private CategorieRepository repository = new CategorieRepository();

        public List<Categorie> SetList()
        {
            categories = repository.SetList();

            return categories;
        }

        public Categorie CreateCategorie(Categorie categorie)
        {
            repository.CreateCategory(categorie);
            return categorie;
        }
        

        public void EditCategorie(Categorie categorie)
        {
            repository.EditCategory(categorie);
        }
    }
}
