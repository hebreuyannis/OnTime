using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Users.Commands;
using OnTime.Application.Users.Queries;
using OnTime.Contracts.Users;
using OnTime.Domain.Comon;
using OnTime.Domain.User;
using System.Net;

namespace OnTime.Api.Controllers;

[Route("[controller]")]
public class UsersController(ISender _mediator, ILogger<UsersController> logger) : BaseController
{

    /// <summary>
    /// Create user 
    /// </summary>
    /// <param name="request"> body</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Success), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.Firstname,request.Lastname,request.Email,request.Password);

        var result = await _mediator.Send(command);

        return result.Match(
            succes => Created(),
            Problem);
    }

    /// <summary>
    /// Get user with id
    /// </summary>
    /// <param name="userId"> Id of user</param>
    /// <returns></returns>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var query = new GetUserQuery(userId);

        var result = await _mediator.Send(query);

        return result.Match(
            Ok,
            Problem);
    }
}
