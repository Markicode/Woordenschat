using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Genres
{
    public class CreateGenreDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentGenreId { get; set; }
    }
}
