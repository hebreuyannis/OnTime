using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnTime.Application.Users.Commands.CreateUser;
using OnTime.Application.Users.Queries.GetAllUserQuery;
using OnTime.Application.Users.Queries.GetUserQuery;
using OnTime.Contracts.Users;
using OnTime.Domain.Comon;
using OnTime.Domain.User;
using System.Net;

namespace OnTime.Api.Controllers;

[Route("[controller]")]
public class UsersController(ISender _mediator, ILogger<UsersController> logger) : BaseController
{
    /// <summary>
    /// Get list of users
    /// </summary>
    /// <returns></returns>
    [HttpGet()]
    [ProducesResponseType(typeof(List<User>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails),(int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllUSer(CancellationToken cancellation)
    {
        var query = new GetAllUserQuery();

        var result = await _mediator.Send(query,cancellation);

        return result.Match(
            Ok,
            Problem);
    }


    /// <summary>
    /// Create user 
    /// </summary>
    /// <param name="request"> body</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Success), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request,CancellationToken cancellation)
    {
        var command = new CreateUserCommand(request.Firstname,request.Lastname,request.Email,request.Password);

        var result = await _mediator.Send(command,cancellation);

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
    public async Task<IActionResult> GetUser(Guid userId,CancellationToken cancellation)
    {
        var query = new GetUserQuery(userId);

        var result = await _mediator.Send(query,cancellation);

        return result.Match(
            Ok,
            Problem);
    }
}
