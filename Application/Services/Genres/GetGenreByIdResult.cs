using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Genres
{
    public class GetGenreByIdResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public Genre? Genre { get; set; }

        private GetGenreByIdResult(bool isSuccess, Genre? genre, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Genre = genre;
            ErrorMessage = errorMessage;
        }

        public static GetGenreByIdResult Success(Genre genre)
        {
            return new GetGenreByIdResult(true, genre, null);
        }

        public static GetGenreByIdResult Failure(string errorMessage)
        {
            return new GetGenreByIdResult(false, null, errorMessage);
        }
    }
}
