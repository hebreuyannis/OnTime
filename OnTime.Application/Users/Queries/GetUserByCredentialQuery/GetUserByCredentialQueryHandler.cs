using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries.GetUserByCredentialQuery;

public class GetUserByCredentialQueryHandler(IUsersRepository _usersRepository) : IRequestHandler<GetUserByCredentialQuery, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(GetUserByCredentialQuery request, CancellationToken cancellationToken)
    {
        return await _usersRepository.GetByCredentialAsync(request.email, request.password,cancellationToken) is User user 
            ? user
            : Error.Unauthorized(description: "The credentials provided are incorrect or invalid. Please double-check your email and password and try again.");
    }
}
