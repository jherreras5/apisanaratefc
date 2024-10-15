using System;
using System.ComponentModel.DataAnnotations;

namespace api_sanarate.Models
{
    public class News
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public required string title { get; set; }

        [Required]
        public required string content { get; set; }

        [StringLength(255)]
        public required string image { get; set; }

        [Required]
        public DateTime published_at { get; set; }

        public DateTime created_at { get; set; } = DateTime.UtcNow;
    }
}
