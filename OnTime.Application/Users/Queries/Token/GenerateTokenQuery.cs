using MediatR;
using OnTime.Domain.Common.Error;

namespace OnTime.Application.Users.Queries.Token;

public record GenerateTokenQuery(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    List<string> Permissions,
    List<string> Roles) : IRequest<ErrorOr<GenerateTokenResult>>;

