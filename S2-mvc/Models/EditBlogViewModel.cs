using BusinessLogicLayer.DTO_s;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;

namespace S2_mvc.Models
{
    public class EditBlogViewModel
    {
        public Blog blog { get; set; }

        public List<Categorie> categories { get; set; }
    }
}
