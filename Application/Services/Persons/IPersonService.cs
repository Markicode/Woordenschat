using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persons
{
    public interface IPersonService
    {
        public Task<Result<List<Person>>> GetPersonsAsync();

        public Task<Result<Person>> GetPersonByIdAsync(int id);

        public Task<Result<Unit>> DeletePersonAsync(int id);
    }
}
