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

            var genres = await response.Content.ReadFromJsonAsync<List<GenreDto>>();

            Assert.NotNull(genres);
            Assert.NotEmpty(genres);
        }
    }
}
