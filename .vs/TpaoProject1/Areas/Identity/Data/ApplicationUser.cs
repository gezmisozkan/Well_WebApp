using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TpaoProject1.Model;

namespace TpaoProject1.Areas.Identity.Data
{
    public class ApplicationUser: IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        public string FirstName { get; set; }
        
        [PersonalData]
        [Column(TypeName="nvarchar(100)")]
        public string LastName { get; set; }


        public ICollection<WellTop> WellTops { get; set; }
    }
}
