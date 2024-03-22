using MediatR;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries;

public record GetUserQuery(Guid userId) : IRequest<ErrorOr<User>>
{
}
