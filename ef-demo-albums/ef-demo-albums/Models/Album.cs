using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ef_demo_albums.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; }

        // Foreign keys
        public int ArtistId { get; set; }
        public int GenreId { get; set; }

        // Navigation properties
        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
