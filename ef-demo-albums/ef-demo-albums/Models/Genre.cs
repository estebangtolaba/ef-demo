using System.ComponentModel.DataAnnotations;

namespace ef_demo_albums.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Album> Albums { get; set; }
    }
}
