using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
