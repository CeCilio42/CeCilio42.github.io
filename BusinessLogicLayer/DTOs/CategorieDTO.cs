using BusinessLogicLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class CategorieDTO
    {
        public CategorieDTO() 
        {

        }

        public CategorieDTO(Categorie categorie) 
        {
            Id = categorie.Id;
            Name = categorie.Title;
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
