using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using DataLogicLayer.DaL;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;

namespace S2_mvc.Controllers
{
    public class CategorieController : Controller
    {
        ICategoryRepository repository = new CategorieRepository();
        

        //Get categories to edit
        public IActionResult EditCategories()
        {
            CategorieService categorieService = new CategorieService(repository);

            EditCategoriesViewModel editCategoriesViewModel = new EditCategoriesViewModel();

            editCategoriesViewModel.categories = categorieService.SetList();

            return View(editCategoriesViewModel);
        }

        //Create category
        public IActionResult CreateCategorie(CategorieDTO categorieDto)
        {
            CategorieService categorieService = new CategorieService(repository);

            categorieService.CreateCategorie(categorieDto);

            return RedirectToAction("EditCategories");
        }

        //Save edit category
        public IActionResult updateCategorie(Categorie categorie)
        {
            CategorieService categorieService = new CategorieService(repository);

            CategorieDTO categorieDto = new CategorieDTO(categorie);
            categorieService.EditCategorie(categorieDto);
            return RedirectToAction("EditCategories");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
