using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly LibraryContext _context;

        public GenreService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<GetGenresResult> GetGenresAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return GetGenresResult.Success(genres);
        }

        public async Task<GetGenreByIdResult> GetGenreByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return GetGenreByIdResult.Failure("Genre not found.");
            }
            return GetGenreByIdResult.Success(genre);
        }
    }
}
