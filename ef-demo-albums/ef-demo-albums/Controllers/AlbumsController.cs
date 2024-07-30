using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ef_demo_albums.Data;
using ef_demo_albums.Dtos;
using ef_demo_albums.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlbumsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlbumsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            // Return albums with artist and genre details
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }
            return album;
        }

        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(AlbumDto albumDto)
        {
            // Validate if Artist and Genre exist
            var artist = await _context.Artists.FindAsync(albumDto.ArtistId);
            var genre = await _context.Genres.FindAsync(albumDto.GenreId);

            if (artist == null || genre == null)
            {
                return BadRequest("Invalid ArtistId or GenreId");
            }

            var album = new Album
            {
                Title = albumDto.Title,
                ReleaseDate = albumDto.ReleaseDate,
                ArtistId = albumDto.ArtistId,
                GenreId = albumDto.GenreId
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = album.AlbumId }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDto albumDto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            // Validate if Artist and Genre exist
            var artist = await _context.Artists.FindAsync(albumDto.ArtistId);
            var genre = await _context.Genres.FindAsync(albumDto.GenreId);

            if (artist == null || genre == null)
            {
                return BadRequest("Invalid ArtistId or GenreId");
            }

            album.Title = albumDto.Title;
            album.ReleaseDate = albumDto.ReleaseDate;
            album.ArtistId = albumDto.ArtistId;
            album.GenreId = albumDto.GenreId;

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumId == id);
        }
    }
}
