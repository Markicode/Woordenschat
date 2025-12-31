
namespace Application.Dtos
{

    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Isbn { get; set; }
        public DateOnly PublishedDate { get; set; }
        public List<GenreDto> Genres { get; set; } = new();
        public List<AuthorDto> Authors { get; set; } = new();

    }
}