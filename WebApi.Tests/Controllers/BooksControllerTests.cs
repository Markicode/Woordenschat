using Application.Dtos;
using Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApi.Tests.Helpers;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Tests.Controllers
{
    public class BooksControllerTests : IClassFixture<ApiFactory>, IAsyncLifetime
    {
        private readonly ApiFactory _factory;
        private readonly HttpClient _client;

        public BooksControllerTests(ApiFactory factory)
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
        public async Task GetBooks_ReturnsBooks()
        {
            // Arrange - create a book first
            var createdBook = await TestData.CreateBook(_client);

            // Act - get all books
            var response = await _client.GetAsync("/api/books");

            // Assert - check if the created book is in the list
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
            books!.Should().Contain(b => b.Id == createdBook!.Id);

        }

        [Fact]
        public async Task GetBookById_ReturnsBook()
        {
            // Arrange - create a book first
            var createdBook = await TestData.CreateBook(_client);

            // Act - get the book by ID
            var response = await _client.GetAsync($"/api/books/{createdBook!.Id}");

            // Assert - check if the fetched book matches the created one
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetchedBook = await response.Content.ReadFromJsonAsync<BookDto>();

            fetchedBook!.Id.Should().Be(createdBook.Id);
            fetchedBook.Title.Should().Be(createdBook.Title);
        }

        #endregion

        #region POST

        [Fact]
        public async Task CreateBook_Returns201()
        {
            // Arrange - Create a new book
            var authorId = await TestData.CreateAuthor(_client);
            var genreId = await TestData.CreateGenre(_client);

            var dto = new
            {
                isbn = TestIsbn.Next(),
                title = "CreateBook Test",
                publishedDate = new DateOnly(2020, 1, 1),
                genreIds = new[] { genreId },
                authorIds = new[] { authorId }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/books", dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await response.Content.ReadFromJsonAsync<BookDto>();
            created!.Title.Should().Be("CreateBook Test");
        }

        #endregion

        #region PUT

        [Fact]
        public async Task ReplaceBook_ReturnsUpdatedBook()
        {
            // Arrange - create a book first
            var createdBook = await TestData.CreateBook(_client);

            // Act - replace the book
            var newAuthorId = await TestData.CreateAuthor(_client);
            var newGenreId = await TestData.CreateGenre(_client);

            var updateDto = new
            {
                isbn = TestIsbn.Next(),
                title = "Updated title",
                description = "Updated description",
                publishedDate = "2021-01-01",
                authorIds = new[] { newAuthorId },
                genreIds = new[] { newGenreId }
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
            var originalAuthorIds = createdBook.Authors.Select(a => a.Id).ToList();
            var originalGenreIds = createdBook.Genres.Select(g => g.Id).ToList();

            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedBook = await putResponse.Content.ReadFromJsonAsync<BookDto>();

            updatedBook.Should().NotBeNull();

            updatedBook!.Title.Should().Be("Updated title");
            updatedBook.Isbn.Should().Be(updateDto.isbn);
            updatedBook.Description.Should().Be("Updated description");
            updatedBook.PublishedDate.Should().Be(new DateOnly(2021, 1, 1));
            updatedBook.Authors.Should().Contain(a => a.Id == newAuthorId);
            updatedBook.Genres.Should().Contain(g => g.Id == newGenreId);
            updatedBook.Authors.Select(a => a.Id)
                .Should().NotIntersectWith(originalAuthorIds);

            updatedBook.Genres.Select(g => g.Id)
                .Should().NotIntersectWith(originalGenreIds);

            var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetchedBook = await getResponse.Content.ReadFromJsonAsync<BookDto>();
            fetchedBook!.Title.Should().Be("Updated title");

        }
        [Fact]
        public async Task ReplaceBook_ReturnsNotFound_ForNonExistentBook()
        {
            // Arrange
            var authorId = await TestData.CreateAuthor(_client);
            var genreId = await TestData.CreateGenre(_client);

            var updateDto = new
            {
                isbn = TestIsbn.Next(),
                title = "Updated title",
                description = "Updated description",
                publishedDate = "2021-01-01",
                authorIds = new[] { authorId },
                genreIds = new[] { genreId }
            };

            var nonExistentBookId = int.MaxValue;

            // Act
            var response = await _client.PutAsJsonAsync(
                $"/api/books/{nonExistentBookId}",
                updateDto
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        #endregion

        #region DELETE

        [Fact]
        public async Task DeleteBook_RemovesBookAndReturnsNoContent()
        {
            // Arrange
            var createdBook = await TestData.CreateBook(_client);

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/books/{createdBook.Id}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_ForNonExistentBook()
        {
            // Arrange
            var nonExistentBookId = int.MaxValue;

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/books/{nonExistentBookId}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region PATCH

        /*[Fact]
        public async Task PatchBook_UpdatesTitle_WithoutAffectingOtherFields()
        {
            // Arrange - create a book first
            var bookDto = new
            {
                isbn = TestIsbn.Next(),
                title = "Book to be patched",
                description = "This book will be patched in the test",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };
            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            var originalDescription = createdBook.Description;
            var originalIsbn = createdBook.Isbn;
            var originalAuthors = createdBook.Authors.Select(a => a.Id).ToList();
            var originalGenres = createdBook.Genres.Select(g => g.Id).ToList();

            // Act - patch the book's title
            var patchBookDto = new
            {
                title = "Patched Book Title"
            };

            var patchResponse = await _client.PatchAsJsonAsync(
                $"/api/books/{createdBook!.Id}",
                patchBookDto
            );

            // Assert - verify the book's title was updated
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var patchedBook = await patchResponse.Content.ReadFromJsonAsync<BookDto>();
            patchedBook!.Title.Should().Be("Patched Book Title");
            patchedBook.Description.Should().Be(originalDescription);
            patchedBook.Isbn.Should().Be(originalIsbn);
            patchedBook.Authors.Select(a => a.Id).Should().BeEquivalentTo(originalAuthors);
            patchedBook.Genres.Select(g => g.Id).Should().BeEquivalentTo(originalGenres);
        }*/
        [Fact]
        public async Task PatchBook_UpdatesTitle_WithoutAffectingOtherFields()
        {
            // Arrange
            var createdBook = await TestData.CreateBook(_client);

            var originalDescription = createdBook.Description;
            var originalIsbn = createdBook.Isbn;
            var originalAuthors = createdBook.Authors.Select(a => a.Id).ToList();
            var originalGenres = createdBook.Genres.Select(g => g.Id).ToList();

            var patchDto = new
            {
                title = "Patched Book Title"
            };

            // Act
            var patchResponse = await _client.PatchAsJsonAsync(
                $"/api/books/{createdBook.Id}",
                patchDto
            );

            // Assert
            patchResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var patchedBook = await patchResponse.Content.ReadFromJsonAsync<BookDto>();

            patchedBook!.Title.Should().Be("Patched Book Title");
            patchedBook.Description.Should().Be(originalDescription);
            patchedBook.Isbn.Should().Be(originalIsbn);
            patchedBook.Authors.Select(a => a.Id).Should().BeEquivalentTo(originalAuthors);
            patchedBook.Genres.Select(g => g.Id).Should().BeEquivalentTo(originalGenres);
        }

        [Fact]
        public async Task PatchBook_ReturnsBadRequest_OnEmptyBody()
        {
            // Arrange
            var createdBook = await TestData.CreateBook(_client);

            var emptyDto = new { };

            // Act
            var patchResponse = await _client.PatchAsJsonAsync(
                $"/api/books/{createdBook.Id}",
                emptyDto
            );

            // Assert
            patchResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*[Fact]
        public async Task PatchBook_ReturnsBadRequest_WhenGenreDoesNotExist()
        {
            // Arrange - create a book and prepare invalid patch DTO
            var bookDto = new
            {
                isbn = TestIsbn.Next(),
                title = "Book to be patched",
                description = "This book will be patched in the test",
                publishedDate = "2022-02-02",
                genreIds = new[] { 24 },
                authorIds = new[] { 1 }
            };
            
            var createResponse = await _client.PostAsJsonAsync("/api/books", bookDto);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdBook = await createResponse.Content.ReadFromJsonAsync<BookDto>();
            createdBook.Should().NotBeNull();

            var originalGenreIds = createdBook!.Genres.Select(g => g.Id).ToList();

            var patchDto = new
            {
                genreIds = new[] { int.MaxValue }
            };

            // Act - attempt to patch with non-existent genre ID

            var response = await _client.PatchAsJsonAsync(
                $"/api/books/{createdBook.Id}",
                patchDto
            );

            // Assert - verify BadRequest is returned
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var unpatchedBookResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            unpatchedBookResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var unpatchedBook = await unpatchedBookResponse.Content.ReadFromJsonAsync<BookDto>();
            unpatchedBook!.Genres.Select(g => g.Id).Should().BeEquivalentTo(originalGenreIds);

        }*/
        [Fact]
        public async Task PatchBook_ReturnsBadRequest_WhenGenreDoesNotExist()
        {
            // Arrange
            var createdBook = await TestData.CreateBook(_client);

            var originalGenreIds = createdBook.Genres.Select(g => g.Id).ToList();

            var patchDto = new
            {
                genreIds = new[] { int.MaxValue }
            };

            // Act
            var response = await _client.PatchAsJsonAsync(
                $"/api/books/{createdBook.Id}",
                patchDto
            );

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var getResponse = await _client.GetAsync($"/api/books/{createdBook.Id}");
            var unpatchedBook = await getResponse.Content.ReadFromJsonAsync<BookDto>();

            unpatchedBook!.Genres.Select(g => g.Id)
                .Should().BeEquivalentTo(originalGenreIds);
        }

        #endregion

    }
}
