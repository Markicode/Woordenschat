using Application.Dtos;
using Application.Dtos.Genres;
using Application.Services.Books;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.Helpers
{
    public class TestData
    {
        public static async Task<int> CreateAuthor(HttpClient client)
        {
            var dto = new
            {
                firstName = "Test",
                lastName = Guid.NewGuid().ToString(),
                birthDate = "1990-01-01"
            };

            var response = await client.PostAsJsonAsync("/api/authors", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"CreateAuthor failed: {error}");
            }

            var created = await response.Content.ReadFromJsonAsync<AuthorDto>();
            return created!.Id;
        }

        public static async Task<int> CreateGenre(HttpClient client)
        {
            var dto = new
            {
                name = "Genre-" + Guid.NewGuid()
            };

            var response = await client.PostAsJsonAsync("/api/genres", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"CreateGenre failed: {error}");
            }

            var created = await response.Content.ReadFromJsonAsync<GenreWithParentDto>();
            return created!.Id;
        }

        public static async Task<BookDto> CreateBook(HttpClient client)
        {
            var authorId = await CreateAuthor(client);
            var genreId = await CreateGenre(client);

            var dto = new
            {
                isbn = TestIsbn.Next(),
                title = "Test Title",
                description = "Test Description",
                publishedDate = new DateOnly(2000, 1, 1),
                authorIds = new List<int> { authorId },
                genreIds = new List<int> { genreId }
            };
            var response = await client.PostAsJsonAsync("/api/books", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"CreateGenre failed: {error}");
            }

            var createdBook = await response.Content.ReadFromJsonAsync<BookDto>();
            return createdBook!;
        }
    }
}
