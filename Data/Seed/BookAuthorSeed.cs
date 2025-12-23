using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Seed
{
    public static class BookAuthorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasData(
                new BookAuthor
                {
                    BookId = 1,
                    AuthorId = 5
                },
                new BookAuthor
                {
                    BookId = 2,
                    AuthorId = 6
                },
                new BookAuthor
                {
                    BookId = 3,
                    AuthorId = 1
                },
                new BookAuthor
                {
                    BookId = 4,
                    AuthorId = 2
                },
                new BookAuthor
                {
                    BookId = 5,
                    AuthorId = 7
                }
            );
        }
    }
}
