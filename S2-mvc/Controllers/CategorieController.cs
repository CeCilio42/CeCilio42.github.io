using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces_Services;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;

namespace S2_mvc.Controllers
{
    public class CategorieController : Controller
    {


        private readonly ICategorieService categorieService;

        public CategorieController(ICategorieService _categorieService)
        {
            categorieService = _categorieService;
        }

        //Get categories to edit
        public IActionResult EditCategories()
        {


            EditCategoriesViewModel editCategoriesViewModel = new EditCategoriesViewModel();

            editCategoriesViewModel.categories = categorieService.SetList();

            return View(editCategoriesViewModel);
        }

        //Create category
        public IActionResult CreateCategorie(Categorie categorie)
        {

            

            categorieService.CreateCategorie(categorie);

            return RedirectToAction("EditCategories");
        }

        //Save edit category
        public IActionResult updateCategorie(Categorie categorie)
        {


            categorieService.EditCategorie(categorie);
            return RedirectToAction("EditCategories");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
