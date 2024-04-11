using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entitys
{
    public class Categorie
    {
        public Categorie() 
        {

        }

        public Categorie(CategorieDTO categorieDTO) 
        { 
            this.Id = categorieDTO.Id;
            this.Title = categorieDTO.Name;
        }


        public int Id { get; set; }
        public string Title { get; set; }


    }
}
