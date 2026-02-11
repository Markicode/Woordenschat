using Application.Common;
using Application.Enums;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persons
{
    public class PersonService : IPersonService
    {
        private readonly LibraryContext _context;

        public PersonService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Person>>> GetPersonsAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            return Result<List<Person>>.Success(persons);
        }

        public async Task<Result<Person>> GetPersonByIdAsync(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Family)
                .Include(p => p.Member)
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Result<Person>.Failure(ErrorType.NotFound, $"Person with ID {id} not found.");
            }


            return Result<Person>.Success(person);
        }

        public async Task<Result<Unit>> DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return Result<Unit>.Failure(ErrorType.NotFound, "Person not found.");
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
