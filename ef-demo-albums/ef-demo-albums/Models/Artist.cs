using System.ComponentModel.DataAnnotations;

namespace ef_demo_albums.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Album> Albums { get; set; }
    }
}
