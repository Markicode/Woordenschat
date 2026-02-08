using System.Net;
using System.Net.Http.Json;
using Application.Dtos.Genres;
using FluentAssertions;
using WebApi.Tests.Helpers;
using Xunit;

namespace WebApi.Tests.Controllers
{
    public class GenresControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
    {
        private readonly ApiFactory _factory;
        private readonly HttpClient _client;

        public GenresControllerTests(ApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _factory.ResetDatabaseAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task GetGenres_ReturnsListOfGenres()
        {
            // Arrange
            var createdGenreId = await TestData.CreateGenre(_client);

            // Act
            var response = await _client.GetAsync("api/genres");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var genres = await response.Content.ReadFromJsonAsync<List<GenreWithParentDto>>();
            genres.Should().Contain(g => g.Id == createdGenreId);
        }

        [Fact]
        public async Task GetGenreById_ReturnsGenre()
        {
            // Arrange
            var createdGenreId = await TestData.CreateGenre(_client);

            // Act
            var response = await _client.GetAsync($"api/genres/{createdGenreId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var genre = await response.Content.ReadFromJsonAsync<GenreWithParentDto>();
            genre!.Id.Should().Be(createdGenreId);
        }

        [Fact]
        public async Task GetGenreById_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("api/genres/99999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetGenres_AllParentGenresValid()
        {
            // Arrange
            var parentId = await TestData.CreateGenre(_client);

            var childDto = new
            {
                name = "ChildGenre",
                parentGenreId = parentId
            };

            var createChild = await _client.PostAsJsonAsync("/api/genres", childDto);
            createChild.EnsureSuccessStatusCode();

            // Act
            var response = await _client.GetAsync("api/genres");
            var genres = await response.Content.ReadFromJsonAsync<List<GenreWithParentDto>>();

            // Assert
            genres!.Should().Contain(g => g.Id == parentId);
            genres.Should().Contain(g => g.ParentGenreId == parentId);
        }
    }
}