using Microsoft.AspNetCore.Identity;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Model;
using X.PagedList;

namespace TpaoProject1.Models
{
    public class PageUserModel
    {
         public List<ApplicationUser> Kullanicilar { get; set; }
        public List<IdentityRole> Roller { get; set; }
        public List<Formation> Formasyon { get; set; }
        public IPagedList<WellTop> Kuyular { get; set; }
        public List<WellTop> MapKuyular { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
    }
}

