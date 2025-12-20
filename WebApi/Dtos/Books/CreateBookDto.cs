
namespace WebApi.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public List<int> GenreIds { get; set; } = new();

    }
}

