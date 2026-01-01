using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Application.Dtos;
using FluentAssertions;

namespace WebApi.Tests.Controllers
{
    public class BooksControllerTests : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public BooksControllerTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetBooks_ReturnsBooks()
        {
            // Arrange - create a book first
            var bookDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "GetBookById Testbook",
                description = "BookById description",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };

            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            // Act - get all books
            var response = await _client.GetAsync("/api/books");

            // Assert - check if the created book is in the list
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
            books.Should().NotBeNull();
            books!.Should().Contain(b => b.Id == createdBook!.Id);

        }

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            // Arrange - create a book first
            var bookDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "GetBookById Testbook",
                description = "BookById description",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };

            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            // Act - get the book by ID
            var response = await _client.GetAsync($"/api/books/{createdBook!.Id}");

            // Assert - check if the fetched book matches the created one
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetchedBook = await response.Content.ReadFromJsonAsync<BookDto>();
            fetchedBook.Should().NotBeNull();
            fetchedBook!.Id.Should().Be(createdBook.Id);
            fetchedBook.Title.Should().Be("GetBookById Testbook");
        }

        [Fact]
        public async Task CreateBook_Returns201()
        {
            // Arrange - Create a new book
            var bookDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "Test Book",
                publishedDate = new DateOnly(2023, 1, 1),
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };

            // Act - Post the new book
            var response = await _client.PostAsJsonAsync("/api/books", bookDto);

            // Assert - Check for 201 Created response and Location header
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task ReplaceBook_ReturnsUpdatedBook()
        {
            // Arrange - create a book first
            // TODO: Consider using a factory or builder pattern for creating test data
            var bookDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "Original Book Title",
                description = "Original Description",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };

            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.EnsureSuccessStatusCode();

            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            // Act - replace the book

            var updateDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "Updated title",
                description = "Updated description",
                publishedDate = "2021-01-01",
                authorIds = new[] { 2 },
                genreIds = new[] { 30 }
            };

            var putResponse = await _client.PutAsJsonAsync(
                $"/api/books/{createdBook.Id}",
                updateDto
            );

            if(!putResponse.IsSuccessStatusCode)
            {
                var errorContent = await putResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response content: {errorContent}");
            }

            // Assert - verify the book was updated
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedBook = await putResponse.Content.ReadFromJsonAsync<BookDto>();
            updatedBook!.Title.Should().Be("Updated title");
            updatedBook.Isbn.Should().Be(updateDto.isbn);
            updatedBook.Description.Should().Be("Updated description");
            updatedBook.PublishedDate.Should().Be(new DateOnly(2021, 1, 1));
            updatedBook.Authors.Should().Contain(a => a.Id == 2);
            updatedBook.Genres.Should().Contain(g => g.Id == 30);
            updatedBook.Authors.Should().NotContain(a => a.Id == 1);
            updatedBook.Genres.Should().NotContain(g => g.Id == 24);

            var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetchedBook = await getResponse.Content.ReadFromJsonAsync<BookDto>();
            fetchedBook!.Title.Should().Be("Updated title");


        }

        [Fact]
        public async Task DeleteBook_RemovesBookAndReturnsNoContent()
        {
            // Arrange - create a book first
            var bookDto = new
            {
                isbn = Guid.NewGuid().ToString("N").Substring(0, 13),
                title = "Book to be deleted",
                description = "This book will be deleted in the test",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };
            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            // Act - delete the book
            var deleteResponse = await _client.DeleteAsync($"/api/books/{createdBook!.Id}");

            // Assert - verify the book was deleted
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
