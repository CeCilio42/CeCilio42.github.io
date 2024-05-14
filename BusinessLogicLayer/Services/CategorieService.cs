using System.Diagnostics;
using System.Collections.Generic;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.Entitys;
using BusinessLogicLayer.Entitys;
using System.Net.Http.Headers;

namespace BusinessLogicLayer.Classes
{
    public class CategorieService
    {

        List<Categorie> categories = new List<Categorie>();
        private readonly ICategoryRepository repository;



        public CategorieService(ICategoryRepository repo)
        {
            repository = repo;
        }

        public List<Categorie> SetList()
        {
            var categorieDtos = repository.SetList();
            var categories = categorieDtos.Select(dto => new Categorie(dto)).ToList();
            return categories;
        }


        public Categorie CreateCategorie(Categorie categorie)
        {
            CategorieDTO categorieDto = new CategorieDTO(categorie);
            repository.CreateCategory(categorieDto);

            return categorie;
        }

        public void EditCategorie(Categorie categorie)
        {
            CategorieDTO categoryDto = new CategorieDTO(categorie);
            repository.EditCategory(categoryDto);
        }

    }
}
