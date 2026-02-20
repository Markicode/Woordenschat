using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    public static class BookSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Description = "A novel written by American author F. Scott Fitzgerald.",
                    Isbn = new Isbn("9780743273565"),
                    PublishedDate = new DateOnly(1925, 4, 10)
                },
                new 
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "A novel by Harper Lee published in 1960.",
                    Isbn = new Isbn("9780061120084"),
                    PublishedDate = new DateOnly(1960, 7, 11)
                },
                new 
                {
                    Id = 3,
                    Title = "1984",
                    Description = "A dystopian social science fiction novel and cautionary tale by the English writer George Orwell.",
                    Isbn = new Isbn("9780451524935"),
                    PublishedDate = new DateOnly(1949, 6, 8)
                },
                new 
                {
                    Id = 4,
                    Title = "Pride and Prejudice",
                    Description = "A romantic novel of manners written by Jane Austen.",
                    Isbn = new Isbn("9780141439518"),
                    PublishedDate = new DateOnly(1813, 1, 28)
                },
                new 
                {
                    Id = 5,
                    Title = "The Da Vinci Code",
                    Description = "A mystery thriller novel by Dan Brown.",
                    Isbn = new Isbn("9780307474278"),
                    PublishedDate = new DateOnly(2003, 4, 1)
                }
            );
        }
    }
}
