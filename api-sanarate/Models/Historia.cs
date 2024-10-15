using System;
using System.ComponentModel.DataAnnotations;

namespace api_sanarate.Models
{
    public class Historia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string evento { get; set; }

        [Required]
        public required string descripcion { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [StringLength(255)]
        public required string imagen { get; set; }
    }
}
