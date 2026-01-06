using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Enums;

namespace Application.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context) 
        { 
            _context = context;
        }

        public async Task<Result<List<Author>>> GetAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return Result<List<Author>>.Success(authors);
        }

        public async Task<Result<Author>> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return Result<Author>.Failure(ErrorType.NotFound, $"Author with ID {id} not found.");
            }

            return Result<Author>.Success(author);
        }

        public async Task<Result<Author>> CreateAuthorAsync(CreateAuthorCommand createAuthorCommand)
        {


            var author = new Author
            {
                FirstName = createAuthorCommand.FirstName,
                LastName = createAuthorCommand.LastName,
                Bio = createAuthorCommand.Bio,
                BirthDate = createAuthorCommand.BirthDate
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Result<Author>.Success(author);
        }

        public async Task<Result<Author>> ReplaceAuthorAsync(ReplaceAuthorCommand replaceAuthorCommand)
        {
            // Implementation for updating an existing author
            throw new NotImplementedException();
        }

        public async Task<Result<Unit>> DeleteAuthorAsync(int id)
        {
            // Implementation for deleting an author
            throw new NotImplementedException();
        }

        public async Task<Result<Author>> PatchAuthorAsync(PatchAuthorCommand patchAuthorCommand)
        {
            // Implementation for partially updating an author
            throw new NotImplementedException();
        }
    }


}
