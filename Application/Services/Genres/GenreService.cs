using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application;
using Application.Common;
using Application.Enums;

namespace Application.Services.Genres
{
    public class GenreService : IGenreService
    {
        private readonly LibraryContext _context;

        public GenreService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Genre>>> GetGenresAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return Result<List<Genre>>.Success(genres);
        }

        public async Task<Result<Genre>> GetGenreByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return Result<Genre>.Failure(ErrorType.NotFound, "Genre not found.");
            }
            return Result<Genre>.Success(genre);
        }
    }
}
