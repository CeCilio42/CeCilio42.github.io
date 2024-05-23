using BusinessLogicLayer.Classes;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;
using S2_mvcUnitTest.FakeDaL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.EntityTests
{
    [TestClass]
    public class CategorieTest
    {
        private CategorieService service;

        public CategorieTest()
        {
            service = new CategorieService(new FakeCategoryRepo());
        }

        [TestMethod]
        public void SetList_ShouldReturnCorrectCategories()
        {
            // Act
            List<Categorie> result = service.SetList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test1", result[0].Title);
            Assert.AreEqual("Test2", result[1].Title);
        }

        [TestMethod]
        public void CreateCategory_ShouldAddNewCategory()
        {
            // Arrange
            Categorie newCategory = new Categorie { Title = "Test3" };

            // Act
            service.CreateCategorie(newCategory);
            List<Categorie> result = service.SetList();

            // Assert
            Assert.AreEqual("Test3", result[2].Title);
        }

        [TestMethod]
        public void EditCategory_ShouldUpdateCategory()
        {
            // Arrange
            Categorie updatedCategory = new Categorie { Id = 1, Title = "UpdatedTest1" };

            // Act
            service.EditCategorie(updatedCategory);
            List<Categorie> result = service.SetList();

            // Assert
            Assert.AreEqual("UpdatedTest1", result[0].Title);
        }
    }
}
