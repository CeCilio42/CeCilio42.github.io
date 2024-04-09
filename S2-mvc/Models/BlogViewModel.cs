using BusinessLogicLayer.Entitys;
using DataAccessLayer.Entitys;
using DataLogicLayer.Entitys;

namespace S2_mvc.Models
{
    public class BlogViewModel
    {
        public List<Blog>? BlogList { get; set; }

        public List<Categorie>? categories { get; set; }

        public List<Blog> OwnerList { get; set; }

        public User User { get; set; }
    }
}
