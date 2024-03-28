using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Users.Queries.GetUserQuery;
using OnTime.Domain.User;
using OnTime.Infrastructure.UserProvider;
using System.Net;

namespace OnTime.Api.Controllers;

[Authorize]
[Route("[controller]")]
public class MeController(ILogger<MeController> _logger, ICurrentUserProvider _currentUserProvider, ISender _mediator) : BaseController
{
    /// <summary>
    /// Get Current user information
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var user = _currentUserProvider.GetCurrentUser();

        var query = new GetUserQuery(user.Id);

        var result = await _mediator.Send(query,cancellationToken);

        return result.Match(Ok, Problem);
    }
}
