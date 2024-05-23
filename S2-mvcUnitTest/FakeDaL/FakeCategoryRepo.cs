using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.FakeDaL
{
    public class FakeCategoryRepo : ICategoryRepository
    {
        private List<Categorie> categories;

        public FakeCategoryRepo()
        {
            categories = new List<Categorie>();

            categories.Add(new Categorie { Id = 1, Title = "Test1" });
            categories.Add(new Categorie { Id = 2, Title = "Test2" });
        
        }

        public List<CategorieDTO> SetList()
        {
            List<CategorieDTO> categoryDTOs = new List<CategorieDTO>();

            foreach (var category in categories)
            {
                categoryDTOs.Add(new CategorieDTO { Id = category.Id, Name = category.Title });
            }

            return categoryDTOs;
        }

        public void CreateCategory(CategorieDTO categorieDTO)
        {
            var newCategory = new Categorie
            {
                Id = categories.Max(c => c.Id) + 1,
                Title = categorieDTO.Name
            };

            categories.Add(newCategory);
        }

        public void EditCategory(CategorieDTO categorieDTO)
        {
            var category = categories.FirstOrDefault(c => c.Id == categorieDTO.Id);
            if (category != null)
            {
                category.Title = categorieDTO.Name;
            }
        }
    }
}
