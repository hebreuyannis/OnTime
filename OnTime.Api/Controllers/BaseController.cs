using Microsoft.AspNetCore.Mvc;
using OnTime.Domain.Common.Error;

namespace OnTime.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected ActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        return Problem(errors[0]);
    }

    private ObjectResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        var problemDetails = ProblemDetailsFactory.CreateProblemDetails(
                HttpContext,
                statusCode: statusCode,
                title: error.Description);

#pragma warning disable CS8619 // La nullabilité des types référence dans la valeur ne correspond pas au type cible.
        problemDetails.Extensions = error.Metadata ?? problemDetails.Extensions;
#pragma warning restore CS8619 // La nullabilité des types référence dans la valeur ne correspond pas au type cible.

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}
