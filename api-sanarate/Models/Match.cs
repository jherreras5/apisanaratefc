using System;
using System.ComponentModel.DataAnnotations;

namespace api_sanarate.Models
{
    public class Match
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int team1_id { get; set; }

        [Required]
        public int team2_id { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        [StringLength(255)]
        public required string venue { get; set; }

        [StringLength(10)]
        public required string score { get; set; }

        [Required]
        public required string status { get; set; }

        public DateTime created_at { get; set; }
    }
}
