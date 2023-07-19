using System.ComponentModel.DataAnnotations;

namespace TpaoProject1.Model
{
    public class Formation
    {
        public Formation(int well_id)
        {
            wellid = well_id;
        }
        public Formation()
        {

        }
        [Key]
        public int Id { get; set; }

        public int wellid { get; set; }

        public string? Form_type { get; set; }
        public int? Form_meter { get; set; }


    }
}