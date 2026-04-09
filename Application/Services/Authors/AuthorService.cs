using Application.Common;
using Application.Enums;
using Application.Services.Books;
using Data;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var author = new Author(createAuthorCommand.FirstName, createAuthorCommand.LastName, createAuthorCommand.Bio, createAuthorCommand.BirthDate);

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Result<Author>.Success(author);
        }

        public async Task<Result<Author>> ReplaceAuthorAsync(ReplaceAuthorCommand replaceAuthorCommand)
        {

            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == replaceAuthorCommand.Id);

            if (author == null)
            {
                return Result<Author>.Failure(ErrorType.NotFound, "Author not found.");
            }

            try
            {
                author.ReplaceDetails(replaceAuthorCommand.FirstName, replaceAuthorCommand.LastName, replaceAuthorCommand.Bio, replaceAuthorCommand.BirthDate);
                await _context.SaveChangesAsync();
                return Result<Author>.Success(author);
            }
            catch (InvalidOperationException ex)
            {
                return Result<Author>.Failure(ErrorType.ValidationError, ex.Message);
            }
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

        /*private async Task<Result<List<Book>>> LoadBooks(IEnumerable<int> bookIds)
        {
            if (authorIds == null || !authorIds.Any())
                return Result<List<Author>>.Failure(ErrorType.ValidationError, "At least one author ID must be provided.");

            var distinctIds = authorIds.Distinct().ToList();

            var authors = await _context.Authors
                .Where(a => distinctIds.Contains(a.Id))
                .ToListAsync();

            if (authors.Count != distinctIds.Count)
                return Result<List<Author>>.Failure(ErrorType.ValidationError, "One or more author IDs are invalid.");

            return Result<List<Author>>.Success(authors);
        }
        */
    }


}
