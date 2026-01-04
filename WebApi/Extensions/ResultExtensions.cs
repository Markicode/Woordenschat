using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Common;

namespace WebApi.Extensions 
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result, ControllerBase controller)
        {
            if(result.IsSuccess)
            {
                throw new InvalidOperationException("ToActionResult can only be called on failed results.");
            }
            
            return result.ErrorType switch
            {
                ErrorType.NotFound => controller.NotFound(result.ErrorMessage),
                ErrorType.ValidationError => controller.BadRequest(result.ErrorMessage),
                ErrorType.Unauthorized => controller.Unauthorized(),
                ErrorType.Forbidden => controller.Forbid(),
                ErrorType.Conflict => controller.Conflict(result.ErrorMessage),
                _ => controller.Problem(result.ErrorMessage)
            };
        }
    }
}
