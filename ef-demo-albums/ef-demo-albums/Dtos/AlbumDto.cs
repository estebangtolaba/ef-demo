namespace ef_demo_albums.Dtos
{
    public class AlbumDto
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
    }
}
