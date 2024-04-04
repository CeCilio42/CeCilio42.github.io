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
        private CategorieRepository database = new CategorieRepository();

        public List<Categorie> SetList()
        {
            categories = database.SetList();

            CheckList(categories);
            return categories;
        }


        private void CheckList(List<Categorie> categories)
        {
            List<Categorie> itemsToRemove = new List<Categorie>();
            foreach (Categorie categorie in categories)
            {
                if (categorie.Title.Length < 5)
                {
                    itemsToRemove.Add(categorie);
                }
            }

            foreach (Categorie itemToRemove in itemsToRemove)
            {
                categories.Remove(itemToRemove);
            }
        }

    }
}
