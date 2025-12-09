using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ParentGenreId { get; set; }
        public Genre? ParentGenre { get; set; }

        public List<Genre> SubGenres { get; set; } = new();

        public List<Book> Books { get; set; } = new();
    }
}
