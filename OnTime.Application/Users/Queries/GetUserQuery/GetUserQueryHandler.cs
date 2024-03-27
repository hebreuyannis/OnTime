using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries.GetUserQuery;

public class GetUserQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await usersRepository.GetByIdAsync(request.userId, cancellationToken) is User user
            ? user
            : Error.NotFound(description: $"User with id {request.userId} not found");
    }
}
