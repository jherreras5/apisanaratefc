using System;
using System.ComponentModel.DataAnnotations;

namespace api_sanarate.Models
{
    public class Team
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public required string name { get; set; }

        [StringLength(255)]
        public required string logo { get; set; }

        public required string description { get; set; }

        public DateTime created_at { get; set; }
    }
}
