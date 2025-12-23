using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    public static class AuthorSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Orwell",
                    BirthDate = new DateTime(1903, 6, 25)
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Austen",
                    BirthDate = new DateTime(1775, 12, 16)
                },
                new Author
                {
                    Id = 3,
                    FirstName = "Mark",
                    LastName = "Twain",
                    BirthDate = new DateTime(1835, 11, 30)
                },
                new Author
                {
                    Id = 4,
                    FirstName = "J.K.",
                    LastName = "Rowling",
                    BirthDate = new DateTime(1965, 7, 31)
                },
                new Author
                {
                    Id = 5,
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald",
                    BirthDate = new DateTime(1896, 9, 24)
                },
                new Author
                {
                    Id = 6,
                    FirstName = "Harper",
                    LastName = "Lee",
                    BirthDate = new DateTime(1926, 4, 28)
                },
                new Author
                {
                    Id = 7,
                    FirstName = "Dan",
                    LastName = "Brown",
                    BirthDate = new DateTime(1964, 6, 22)
                }
            );
        }
    }
}
