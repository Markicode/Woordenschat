using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Mappings
{
    public static class BookMappings
    {
        public static BookDto ToDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Isbn = book.Isbn,
                PublishedDate = book.PublishedDate,
                Genres = book.Genres
                    .OrderBy(g => g.Name)
                    .Select(g => g.ToDto())
                    .ToList(),
                Authors = book.Authors
                    .OrderBy(a => a.LastName)
                    .ThenBy(a => a.FirstName)
                    .Select(a => new AuthorDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    })
                    .ToList()
            };
        }
    }
}
