namespace WebApi.Dtos
{
    public class CreateBookDto
    {

    }
}

public class CreateBookDto
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? PublishedDate { get; set; }

    public List<int> AuthorIds { get; set; } = new();
    public List<int> GenreIds { get; set; } = new();
}
