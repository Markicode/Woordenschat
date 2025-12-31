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
            // Act
            var response = await _client.GetAsync("/api/books");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
            Assert.NotNull(books);
            
        }

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            var bookId = 1; // Assuming a book with ID 1 exists in the test database
            // Act
            var response = await _client.GetAsync($"/api/books/{bookId}");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateBook_Returns201()
        {
            var dto = new
            {
                title = "Test Book",
                publishedDate = new DateOnly(2023, 1, 1),
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };

            var response = await _client.PostAsJsonAsync("/api/books", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task ReplaceBook_ReturnsUpdatedBook()
        {
            // arrange - create a book first
            // TODO: Consider using a factory or builder pattern for creating test data
            var bookDto = new
            {
                isbn = "0000000000000",
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

            // act - replace the book

            var updateDto = new
            {
                isbn = "1111111111111",
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

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedBook = await putResponse.Content.ReadFromJsonAsync<BookDto>();
            updatedBook!.Title.Should().Be("Updated title");
            updatedBook.Isbn.Should().Be("1111111111111");
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
    }
}
