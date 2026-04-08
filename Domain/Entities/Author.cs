using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public string? Bio { get; private set; } 
        public DateOnly? BirthDate { get; private set; }

        private readonly List<Book> _books = new();
        public IReadOnlyCollection<Book> Books => _books;

        private Author() { }

        public Author(string firstName, string lastName, string? bio, DateOnly? birthDate)
        {
            ReplaceDetails(firstName, lastName, bio, birthDate);
        }

        public void UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidOperationException("First name can not be empty."); // TODO: Domain exception ipv Invalid operation implementeren.
            if (firstName.Length > 100)
                throw new InvalidOperationException("First name can not be over 100 characters.");

            FirstName = firstName;
        }

        public void UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidOperationException("Last name can not be empty.");
            if (lastName.Length > 100)
                throw new InvalidOperationException("Last name can not be over 100 characters.");

            LastName = lastName;
        }

        public void UpdateBio(string? bio) 
        {
            if (bio != null && bio.Length > 2000)
                throw new InvalidOperationException("Biography can not be over 2000 characters.");

            Bio = bio;                
        }

        public void UpdateBirthDate(DateOnly? birthDate)
        {
            if (birthDate.HasValue && birthDate.Value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidOperationException("Birth date can not be in the future");

            BirthDate = birthDate;
        }

        public void ReplaceDetails(string firstName, string lastName, string? bio, DateOnly? birthDate)
        {
            UpdateFirstName(firstName);
            UpdateLastName(lastName);
            UpdateBio(bio);
            UpdateBirthDate(birthDate);
        }

        public void AddBook(Book book)
        {
            if (!_books.Any(b => b.Id == book.Id))
                _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _books.RemoveAll(b => b.Id == book.Id);
        }

    }

}


