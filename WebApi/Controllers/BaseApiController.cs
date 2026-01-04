using Application.Common;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult FromResult(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("FromResult should not be used for successful results.");
            }

            return result.ToActionResult(this);
        }
    }
}
