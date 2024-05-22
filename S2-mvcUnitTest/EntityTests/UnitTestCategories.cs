using BusinessLogicLayer;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.DaL;

namespace S2_mvcUnitTest.EntityTests
{
    [TestClass]
    public class UnitTestCategories
    {
        CategorieService categorieService = new CategorieService(new CategorieRepository());

        [TestMethod]
        public void Categorie_GetCategories_CategoryListInNotNull()
        {
            List<Categorie> categories = categorieService.SetList();

            Assert.IsNotNull(categories);

        }


        [TestMethod]
        public void Categorie_DeleteCategorie_DeleteCategorieIsTrue()
        {
            //add categorie properties

            //delete categorie from program

            //check the output
        }


        [TestMethod]
        public void Categorie_EditCategorie_EditCategorieIsTrue()
        {
            //add categorie properties

            //edit categorie

            //check the output
        }
    }
}