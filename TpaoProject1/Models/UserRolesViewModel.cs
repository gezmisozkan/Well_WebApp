using Microsoft.AspNetCore.Identity;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Model;

namespace TpaoProject1.Models
{
    public class UserRolesViewModel
    {
        public List<ApplicationUser> Kullanicilar { get; set; }
        public List<IdentityRole> Roller { get; set; }
        public List<Formation> Formasyon { get; set; }
        public List<WellTop> Kuyular { get; set; }
    }
}
