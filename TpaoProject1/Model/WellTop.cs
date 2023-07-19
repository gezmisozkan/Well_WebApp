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
		[Required]
		[MaxLength(22, ErrorMessage = "Kuyu adı en fazla 22 karakter içerebilir!")]
		[RegularExpression(@"^([A-Z]+(?: [A-Z]+)?)-([0-9]+)+$", ErrorMessage = "Lütfen istenen formatta giriniz!")]
		public string Name { get; set; }
		[Required]
		[RegularExpression(@"^(2[6-9]|4[0-5]|3[0-9])(\.[0-9]{6})$", ErrorMessage = "Girilen enlem koordinat tipi hatalı!")]
		public string Latitude { get; set; }
		[Required]
		[RegularExpression(@"^(3[6-9]|4[0-2])(\.[0-9]{6})$", ErrorMessage = "Girilen boylam koordinat tipi hatalı!")]
		public string Longitude { get; set; }
        

        public string WellTopType { get; set; }
		public string? City { get; set; }
		public DateTime InsertionDate { get; set; } = DateTime.Now;
		public DateTime UpdateDate { get; set; } = DateTime.Now;
		public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }




    }
}


