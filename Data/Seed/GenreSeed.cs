using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seed
{
    public static class GenreSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                // Root genres
                new Genre { Id = 1, Name = "Fiction" },
                new Genre { Id = 2, Name = "Non-Fiction" },
                new Genre { Id = 3, Name = "Programming" },
                new Genre { Id = 4, Name = "Science" },
                new Genre { Id = 5, Name = "History" },
                new Genre { Id = 6, Name = "Children" },

                // Fiction subgenres
                new Genre { Id = 10, Name = "Fantasy", ParentGenreId = 1 },
                new Genre { Id = 11, Name = "Science Fiction", ParentGenreId = 1 },
                new Genre { Id = 12, Name = "Thriller", ParentGenreId = 1 },
                new Genre { Id = 13, Name = "Mystery", ParentGenreId = 1 },
                new Genre { Id = 14, Name = "Romance", ParentGenreId = 1 },
                new Genre { Id = 15, Name = "Horror", ParentGenreId = 1 },

                // Non-Fiction subgenres
                new Genre { Id = 20, Name = "Biography", ParentGenreId = 2 },
                new Genre { Id = 21, Name = "Philosophy", ParentGenreId = 2 },
                new Genre { Id = 22, Name = "Psychology", ParentGenreId = 2 },
                new Genre { Id = 23, Name = "Self-Help", ParentGenreId = 2 },
                new Genre { Id = 24, Name = "Education", ParentGenreId = 2 },

                // Programming subgenres
                new Genre { Id = 30, Name = "Software Development", ParentGenreId = 3 },
                new Genre { Id = 31, Name = "Web Development", ParentGenreId = 3 },
                new Genre { Id = 32, Name = "Databases", ParentGenreId = 3 },
                new Genre { Id = 33, Name = "DevOps", ParentGenreId = 3 },
                new Genre { Id = 34, Name = "Programming Languages", ParentGenreId = 3 },

                // Science subgenres
                new Genre { Id = 40, Name = "Physics", ParentGenreId = 4 },
                new Genre { Id = 41, Name = "Chemistry", ParentGenreId = 4 },
                new Genre { Id = 42, Name = "Biology", ParentGenreId = 4 },
                new Genre { Id = 43, Name = "Mathematics", ParentGenreId = 4 },

                // History subgenres
                new Genre { Id = 50, Name = "Ancient History", ParentGenreId = 5 },
                new Genre { Id = 51, Name = "Medieval History", ParentGenreId = 5 },
                new Genre { Id = 52, Name = "Modern History", ParentGenreId = 5 },

                // Children subgenres
                new Genre { Id = 60, Name = "Picture Books", ParentGenreId = 6 },
                new Genre { Id = 61, Name = "Young Adult", ParentGenreId = 6 },
                new Genre { Id = 62, Name = "Educational (Kids)", ParentGenreId = 6 }
 );
        }
    }
}
