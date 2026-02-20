using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Validation;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }
        public Isbn? Isbn { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public DateOnly? PublishedDate { get; private set; }

        private readonly List<Author> _authors = new();
        public IReadOnlyCollection<Author> Authors => _authors;

        private readonly List<Genre> _genres = new();
        public IReadOnlyCollection<Genre> Genres => _genres;

        private readonly List<BookCopy> _bookCopies = new();
        public IReadOnlyCollection<BookCopy> BookCopies => _bookCopies;

        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations;

        private Book() { }

        public Book(string title, DateOnly? publishedDate, Isbn? isbn, string? description, IEnumerable<Genre> genres, IEnumerable<Author> authors)
        {
            UpdateTitle(title);
            UpdatePublishedDate(publishedDate);
            UpdateIsbn(isbn);
            UpdateDescription(description);
            UpdateAuthors(authors);
            UpdateGenres(genres);
        }
        
        public void UpdateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException("Title cannot be empty");
            if (title.Length > 200)
                throw new InvalidOperationException("Title cannot be over 200 characters.");

            Title = title;
        }

        public void UpdateDescription(string? description)
        {
            if (description?.Length > 2000)
                throw new InvalidOperationException("Description cannot be over 2000 characters.");

            Description = description;
        }

        public void UpdateIsbn(Isbn? isbn)
        {
            Isbn = isbn;
        }

        public void UpdatePublishedDate(DateOnly? publishedDate)
        {
            if (publishedDate.HasValue && publishedDate.Value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidOperationException("Published date cannot be in the future");

            PublishedDate = publishedDate;
        }

        public void UpdateGenres(IEnumerable<Genre> genres)
        {
            if (genres == null || !genres.Any())
                throw new InvalidOperationException("At least one genre is required.");

            _genres.Clear();
            _genres.AddRange(genres);
        }

        public void UpdateAuthors(IEnumerable<Author> authors)
        {
            if (authors == null || !authors.Any())
                throw new InvalidOperationException("At least one author is required.");

            _authors.Clear();
            _authors.AddRange(authors);
        }

        public void ReplaceDetails(string title, DateOnly? publishedDate, Isbn? isbn, string? description, IEnumerable<Genre> genres, IEnumerable<Author> authors)
        {
            UpdateTitle(title);
            UpdatePublishedDate(publishedDate);
            UpdateIsbn(isbn);
            UpdateDescription(description);
            UpdateAuthors(authors);
            UpdateGenres(genres);
        }

    }
}
