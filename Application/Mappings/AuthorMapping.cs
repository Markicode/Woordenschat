using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Dtos.Authors;

namespace Application.Mappings
{
    public static class AuthorMapping
    {
        public static AuthorDto ToDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Bio = author.Bio,
                BirthDate = author.BirthDate
            };
        }

        public static AuthorWithBooksDto ToWithBooksDto(this Author author)
        {
            return new AuthorWithBooksDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Bio = author.Bio,
                BirthDate = author.BirthDate,
                Books = author.Books.Select(b => b.ToDto()).ToList()
            };
        }
    }
}
