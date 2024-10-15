using System.ComponentModel.DataAnnotations;

namespace api_sanarate.Models
{
    public class Player
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int team_id { get; set; }

        [Required]
        [StringLength(100)]
        public required string name { get; set; }

        [Required]
        [StringLength(50)]
        public required string position { get; set; }

        [Required]
        public int number { get; set; }

        [StringLength(255)]
        public required string photo { get; set; }

        public DateTime? created_at { get; set; } // Asegúrate de no establecer manualmente esta propiedad

    }
}
