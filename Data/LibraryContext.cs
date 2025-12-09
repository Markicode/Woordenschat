using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            
        }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
        public DbSet<BookCopy> BookCopies => Set<BookCopy>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Family> Families => Set<Family>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Loan> Loans => Set<Loan>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<NewsPost> NewsPosts => Set<NewsPost>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<User> Users => Set<User>();

    }
}
