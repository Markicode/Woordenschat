using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using WebApi.Dtos;

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
        public async Task GetBooks_ReturnsOkResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/books");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBookById_ReturnsOkResponse()
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
                genreIds = new[] { 24 }
            };

            var response = await _client.PostAsJsonAsync("/api/books", dto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }

    }
}
