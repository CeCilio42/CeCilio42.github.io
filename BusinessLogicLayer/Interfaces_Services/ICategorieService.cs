using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces_Services
{
    public interface ICategorieService
    {
        List<Categorie> SetList();
        Categorie CreateCategorie(Categorie categorie);
        void EditCategorie(Categorie categorie);

    }
}
