using BusinessLogicLayer.Entitys;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_mvcUnitTest.FakeDaL
{
    public class FakeCategoryRepo
    {
        private List<Categorie> categories;

        public FakeCategoryRepo() 
        {
            categories = new List<Categorie>();

            categories.Add(new Categorie { Id = 1, Title = "Test1" });
            categories.Add(new Categorie { Id = 2, Title = "Test2" });

        }
    }
}
