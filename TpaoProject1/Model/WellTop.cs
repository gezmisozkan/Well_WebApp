

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TpaoProject1.Areas.Identity.Data;

namespace TpaoProject1.Model
{
    public class WellTop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WellTopType { get; set; }
        public string City { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime InsertionDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }




    }
}


