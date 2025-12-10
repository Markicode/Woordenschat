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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Author entity configuration 
            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Author>()
                .Property(a => a.Bio)
                .HasMaxLength(2000);

            // Book entity configuration 
            modelBuilder.Entity<Book>()
                .Property(b => b.Isbn)
                .IsRequired()
                .HasMaxLength(13);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(b => b.Description)
                .HasMaxLength(2000);

            // BookAuthor many-to-many join entity configuration 
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // BookCopy entity configuration
            modelBuilder.Entity<BookCopy>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCopy>()
                .Property(bc => bc.Condition)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<BookCopy>()
                .Property(bc => bc.InventoryNumber)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<BookCopy>()
                .HasIndex(bc => bc.InventoryNumber)
                .IsUnique();



        }

    }
}
