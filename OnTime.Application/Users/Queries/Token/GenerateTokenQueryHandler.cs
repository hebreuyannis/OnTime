using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;

namespace OnTime.Application.Users.Queries.Token;

public class GenerateTokenQueryHandler(IJwtTokenGenerator _jwtTokenGenerator) : IRequestHandler<GenerateTokenQuery, ErrorOr<GenerateTokenResult>>
{
    public Task<ErrorOr<GenerateTokenResult>> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
    {
        var token = _jwtTokenGenerator.GenerateToken(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Permissions,
            request.Roles);

        var authenticationResult = new GenerateTokenResult(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            token);

        return Task.FromResult(ErrorOrFactory.From(authenticationResult));
    }
}
