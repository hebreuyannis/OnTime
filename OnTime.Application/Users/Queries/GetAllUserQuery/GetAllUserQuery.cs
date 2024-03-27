using MediatR;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries.GetAllUserQuery;

public record GetAllUserQuery : IRequest<ErrorOr<List<User>>>
{
}
