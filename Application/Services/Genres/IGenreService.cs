using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Genres
{
    public interface IGenreService
    {
        Task<GetGenresResult> GetGenresAsync();

        Task<GetGenreByIdResult> GetGenreByIdAsync(int id);
    }
}
