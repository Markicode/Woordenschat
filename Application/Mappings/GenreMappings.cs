using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Dtos.Genres;

namespace Application.Mappings
{
    public static class GenreMappings
    {
        public static GenreDto ToDto(this Genre genre)
        {
            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        public static GenreWithParentDto ToWithParentDto(this Genre genre)
        {
            return new GenreWithParentDto
            {
                Id = genre.Id,
                Name = genre.Name,
                ParentGenreId = genre.ParentGenreId
            };
        }
    }
}
