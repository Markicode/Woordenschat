using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

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

            // BookAuthor entity configuration 
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

            // Employee entity configuration

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.PersonId)
                .IsUnique();

            // Family entity configuration

            modelBuilder.Entity<Family>()
                .HasMany(f => f.Members)
                .WithOne(m => m.Family)
                .HasForeignKey(m => m.FamilyId);

            modelBuilder.Entity<Family>()
                .Property(f => f.FamilyName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Family>()
                .Property(f => f.Adress)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Family>()
                .Property(f => f.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Family>()
                .Property(f => f.City)
                .IsRequired()
                .HasMaxLength(100);

            // Genre entity configuration

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Genre>()
                .HasOne(g => g.ParentGenre)
                .WithMany(g => g.SubGenres)
                .HasForeignKey(g => g.ParentGenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Loan entity configuration

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.BookCopy)
                .WithMany(bc => bc.Loans)
                .HasForeignKey(l => l.BookCopyId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.MemberId);

            // Member entity configuration

            modelBuilder.Entity<Member>()
                .Property(m => m.MembershipNumber)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.MembershipNumber)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Reservations)
                .WithOne(r => r.Member)
                .HasForeignKey(r => r.MemberId);

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Loans)
                .WithOne(l => l.Member)
                .HasForeignKey(l => l.MemberId);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.PersonId)
                .IsUnique();

            // NewsPost entity configuration

            modelBuilder.Entity<NewsPost>()
                .HasOne(np => np.CreatedByEmployee)
                .WithMany(e => e.CreatedNewsPosts)
                .HasForeignKey(np => np.CreatedByEmployeeId);

            modelBuilder.Entity<NewsPost>()
                .Property(np => np.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<NewsPost>()
                .Property(np => np.Content)
                .IsRequired()
                .HasMaxLength(5000);

            // Person entity configuration

            modelBuilder.Entity<Person>()
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Person>()
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithMany(u => u.Persons)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Member)
                .WithOne(m => m.Person)
                .HasForeignKey<Member>(m => m.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Employee)
                .WithOne(e => e.Person)
                .HasForeignKey<Employee>(e => e.PersonId);


            // Reservation entity configuration

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reservations)
                .HasForeignKey(r => r.MemberId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BookId);
            
            // User entity configuration

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200);
        }

    }
}
