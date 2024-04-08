using BusinessLogicLayer.Entitys;
using DataLogicLayer.Entitys;

namespace S2_mvc.Models
{
    public class BlogViewModel
    {
        public List<Blog>? BlogList { get; set; }

        public List<Categorie>? categories { get; set; }

    }
}
