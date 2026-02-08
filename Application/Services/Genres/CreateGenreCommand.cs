using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Genres
{
    public sealed class CreateGenreCommand
    {
        public string Name { get; }
        public int? ParentGenreId { get; }

        public CreateGenreCommand(string name, int? parentGenreId)
        {
            Name = name;
            ParentGenreId = parentGenreId;
        }
    }
}
