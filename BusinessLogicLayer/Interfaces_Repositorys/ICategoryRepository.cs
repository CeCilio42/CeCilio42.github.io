using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICategoryRepository
    {
        List<CategorieDTO> SetList();
        void CreateCategory(CategorieDTO categorie);
        void EditCategory(CategorieDTO categorie);
    }
}
