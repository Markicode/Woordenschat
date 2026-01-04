using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Services.Authors
{
    public interface IAuthorService
    {
        public Task<Result<List<Author>>> GetAuthorsAsync();

        public Task<Result<Author>> GetAuthorByIdAsync(int id);

        public Task<Result<Author>> CreateAuthorAsync(CreateAuthorCommand createAuthorCommand);

        public Task<Result<Author>> ReplaceAuthorAsync(ReplaceAuthorCommand replaceAuthorCommand);

        public Task<Result<Unit>> DeleteAuthorAsync(int id);

        public Task<Result<Author>> PatchAuthorAsync(PatchAuthorCommand patchAuthorCommand);
    }
}
