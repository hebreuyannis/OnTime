using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Users.Queries.GetUserByCredentialQuery;
using OnTime.Application.Users.Queries.Token;
using OnTime.Contracts.Users;
using OnTime.Domain.User;
using System.Net;

namespace OnTime.Api.Controllers;

[Route("[controller]")]
public class AuthenticationController(ISender _mediator) : BaseController
{

    [HttpPost]
    [ProducesResponseType(typeof(GenerateTokenResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Authenticate(UserAuthenticateRequest request)
    {
        var credentialQuery = new GetUserByCredentialQuery(request.email, request.password);

        var userResult = await _mediator.Send(credentialQuery);

        if (userResult.IsError)
            return Problem(userResult.Errors);

        var user = userResult.Value;

        var queryToken = new GenerateTokenQuery(user.Id, user.FirstName, user.LastName, user.Email, new List<string> { "permission1" }, new List<string> { "role1" });

        var result = await _mediator.Send(queryToken);

        return result.Match(
            Ok,
            Problem
            );
        
    }
}
