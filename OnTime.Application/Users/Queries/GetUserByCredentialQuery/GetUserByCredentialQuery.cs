using MediatR;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries.GetUserByCredentialQuery;

public record GetUserByCredentialQuery(string email, string password) : IRequest<ErrorOr<User>>
{
}
