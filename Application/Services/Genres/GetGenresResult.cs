using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Genres
{
    public class GetGenresResult
    {
        public bool IsSuccess { get; set; }
        public List<Genre>? Genres { get; set; }
        public string? ErrorMessage { get; set; }

        private GetGenresResult(bool isSuccess, List<Genre>? genres, string? errorMessage)
        {
            this.IsSuccess = isSuccess;
            this.Genres = genres;
            this.ErrorMessage = errorMessage;
        }

        public static GetGenresResult Success(List<Genre> genres)
        {
            return new GetGenresResult(true, genres, null);
        }

        public static GetGenresResult Failure(string errorMessage)
        {
            return new GetGenresResult(false, null, errorMessage);
        }
    }
}
