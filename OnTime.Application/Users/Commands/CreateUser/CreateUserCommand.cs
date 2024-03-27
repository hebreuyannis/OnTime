using MediatR;
using OnTime.Domain.Common.Error;
using OnTime.Domain.Comon;

namespace OnTime.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorOr<Success>>
{
}
