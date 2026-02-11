using Application.Services.Persons;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using Application.Dtos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]

    public class PersonsController : BaseApiController
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            var response = await _personService.GetPersonsAsync();

            if (!response.IsSuccess)
            {
                return response.ToActionResult(this);
            }

            var persons = response.Value!;

            // TODO: make dtos 

            return Ok();
        }
    }
}
