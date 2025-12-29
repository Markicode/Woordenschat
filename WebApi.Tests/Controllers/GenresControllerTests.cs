using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Application.Dtos;
using Application.Dtos.Genres;

namespace WebApi.Tests.Controllers
{
    public class GenresControllerTests : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public GenresControllerTests(ApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetGenres_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("api/genres");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetGenres_ReturnsListOfGenres()
        {
            var response = await _client.GetAsync("api/genres");

            response.EnsureSuccessStatusCode();

            var genres = await response.Content.ReadFromJsonAsync<List<GenreWithParentDto>>();

            Assert.NotNull(genres);
            Assert.NotEmpty(genres);

            Assert.All(genres!, genre =>
            {
                Assert.True(genre.Id > 0);
                Assert.False(string.IsNullOrWhiteSpace(genre.Name));
            });
        }

        [Fact]
        public async Task GetGenreById_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("api/genres/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetGenreById_ReturnsGenre()
        {
            var response = await _client.GetAsync("api/genres/1");

            response.EnsureSuccessStatusCode();

            var genre = await response.Content.ReadFromJsonAsync<GenreWithParentDto>();

            Assert.NotNull(genre);

            Assert.True(genre.Id > 0);
            Assert.False(string.IsNullOrWhiteSpace(genre.Name));
        }

        [Fact]
        public async Task GetGenreById_ReturnsNotFound()
        {
            var response = await _client.GetAsync("api/genres/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetGenres_AllParentGenresValid()
        {
            var response = await _client.GetAsync("api/genres");
            response.EnsureSuccessStatusCode();
            
            var genres = await response.Content.ReadFromJsonAsync<List<GenreWithParentDto>>();
            Assert.All(genres!, genre =>
            {
                if (genre.ParentGenreId.HasValue)
                { 
                    var parentGenre = genres!.FirstOrDefault(g => g.Id == genre.ParentGenreId.Value);
                    Assert.NotNull(parentGenre);
                    Assert.True(parentGenre.Id > 0);
                    Assert.False(string.IsNullOrWhiteSpace(parentGenre.Name));
                }
            });
        }
    }
}
