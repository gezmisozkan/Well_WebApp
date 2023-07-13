using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace TpaoProject1.Models
{


    public class Welltops
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(22, ErrorMessage = "Kuyu adı en fazla 22 karakter içerebilir!")]
        [RegularExpression(@"^([A-Z]+(?: [A-Z]+)?)-([0-9]+)+$", ErrorMessage = "Kuyu adı kelime ve sayılar arasında bir '-' işareti içermeli!")]
        public string WelltopName { get; set; }
        [Required]
        [RegularExpression(@"^(3[6-9]|4[0-2])(\.[0-9]{6})$", ErrorMessage = "Girilen enlem koordinat tipi hatalı!")]
        public string Latitude { get; set; }
        [Required]
        [RegularExpression(@"^(2[6-9]|4[0-5]|3[0-9])(\.[0-9]{6})$", ErrorMessage = "Girilen boylam koordinat tipi hatalı!")]
        public string Longitude { get; set; }
        
        public string WelltopType { get; set; }
        public string City { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }

    }


}
