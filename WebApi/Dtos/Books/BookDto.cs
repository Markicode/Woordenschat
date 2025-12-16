using WebApi.Dtos.Genres;

namespace WebApi.Dtos.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Isbn { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<GenreDto> Genres { get; set; } = new();

    }
}
