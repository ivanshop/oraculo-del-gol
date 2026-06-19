using Microsoft.AspNetCore.Mvc;
using Oraculo.Application.Responses;

namespace Oraculo.Api.Exntesions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResult<T>(this Result<T> result)
    {
        return result.Code switch
        {
            CodeResponseType.Ok => new OkObjectResult(result.Value),
            CodeResponseType.Created => new CreatedResult(string.Empty, result.Value),

            CodeResponseType.BadRequest => new BadRequestObjectResult(new { error = result.ErrorMessage }),
            CodeResponseType.NotFound => new NotFoundObjectResult(new { error = result.ErrorMessage }),

            CodeResponseType.Conflict or CodeResponseType.Locked
                => new ConflictObjectResult(new { error = result.ErrorMessage }),

            CodeResponseType.InternalError => new ObjectResult(new { error = "Internal server error" })
                { StatusCode = 500 },
            _ => new ObjectResult(new { error = result.ErrorMessage }) { StatusCode = (int)result.Code }
        };
    }
}