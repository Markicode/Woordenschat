using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace WebApi.Tests.Controllers
{
    public class AuthorsControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
    {
        private readonly ApiFactory _factory;
        private readonly HttpClient _client;

        public AuthorsControllerTests(ApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        #region GET

        [Fact]
        public async Task GetAuthors_ReturnsAuthors()
        {
            // Arrange - Create a new author to ensure there is at least one author
            var createAuthorDto = new
            {
                FirstName = "Test",
                LastName = "Author"
            };

            var createResponse = await _client.PostAsJsonAsync("api/authors", createAuthorDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdAuthor = await createResponse.Content.ReadFromJsonAsync<AuthorDto>();
            createdAuthor.Should().NotBeNull();

            // Act - Send a GET request to the /api/authors endpoint
            var response = await _client.GetAsync("api/authors");

            // Assert - Verify the response
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var authors = await response.Content.ReadFromJsonAsync<List<AuthorDto>>();
            authors.Should().NotBeNull();
            authors!.Should().Contain(a => a.Id == createdAuthor!.Id);
        }

        [Fact]
        public async Task GetAuthorById_ReturnsAuthor()
        {
            // Arrange - Create a new author to retrieve later
            var createAuthorDto = new
            {
                FirstName = "Test",
                LastName = "Author"
            };

            var createResponse = await _client.PostAsJsonAsync("api/authors", createAuthorDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdAuthor = await createResponse.Content.ReadFromJsonAsync<AuthorDto>();
            createdAuthor.Should().NotBeNull();

            // Act - Send a GET request to the /api/authors/{id} endpoint
            var response = await _client.GetAsync($"api/authors/{createdAuthor!.Id}");

            // Assert - Verify the response
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var author = await response.Content.ReadFromJsonAsync<AuthorDto>();

            author.Should().NotBeNull();
            author!.Id.Should().Be(createdAuthor.Id);
            author.FirstName.Should().Be(createAuthorDto.FirstName);
            author.LastName.Should().Be(createAuthorDto.LastName);
            author.Bio.Should().BeNull();
            author.BirthDate.Should().BeNull();
        }

        [Fact]
        public async Task GetAuthorById_NonExistentId_ReturnsNotFound()
        {
            // Arrange - Use a non-existent author ID
            var nonExistentAuthorId = int.MaxValue;

            // Act - Send a GET request to the /api/authors/{id} endpoint
            var response = await _client.GetAsync($"api/authors/{nonExistentAuthorId}");

            // Assert - Verify the response
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region POST

        [Fact]
        public async Task CreateAuthor_ReturnsCreatedAuthor()
        {
            // Arrange - Create a new author DTO
            var createAuthorDto = new
            {
                FirstName = "New",
                LastName = "Author",
                Bio = "This is a test author.",
                BirthDate = new DateOnly(1980, 1, 1)
            };

            // Act - Send a POST request to the /api/authors endpoint
            var response = await _client.PostAsJsonAsync("api/authors", createAuthorDto);

            // Assert - Verify the response
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();

            var createdAuthor = await response.Content.ReadFromJsonAsync<AuthorDto>();
            createdAuthor.Should().NotBeNull();
            createdAuthor!.FirstName.Should().Be(createAuthorDto.FirstName);
            createdAuthor.LastName.Should().Be(createAuthorDto.LastName);
            createdAuthor.Bio.Should().Be(createAuthorDto.Bio);
            createdAuthor.BirthDate.Should().Be(createAuthorDto.BirthDate);
        }

        [Fact]
        public async Task CreateAuthor_ReturnsBadRequest_WhenRequiredFieldsMissing()
        {
            // Arrange - Create a new author with missing required fields
            var createAuthorDto = new
            {
                LastName = "Author" // Missing FirstName
            };

            // Act - Send a POST request to the /api/authors endpoint
            var createResponse = await _client.PostAsJsonAsync("api/authors", createAuthorDto);

            // Assert - Verify the response
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        #endregion
    }
}
