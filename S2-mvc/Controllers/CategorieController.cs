using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DaL;
using DataLogicLayer.DaL;
using Microsoft.AspNetCore.Mvc;
using S2_mvc.Models;

namespace S2_mvc.Controllers
{
    public class CategorieController : Controller
    {
        private readonly CategorieService categorieService;

        public CategorieController()
        {
            categorieService = new CategorieService(new CategorieRepository());
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
