using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Genres
{
    public interface IGenreService
    {
        Task<Result<List<Genre>>> GetGenresAsync();

        Task<Result<Genre>> GetGenreByIdAsync(int id);
    }
}
