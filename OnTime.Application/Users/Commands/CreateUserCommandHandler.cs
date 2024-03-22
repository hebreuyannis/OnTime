using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;
using OnTime.Domain.Comon;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Commands;

public class CreateUserCommandHandler(IUsersRepository usersRepository) : IRequestHandler<CreateUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if(await usersRepository.GetByEmailAsync(request.Email, cancellationToken) is not null) 
        {
            return Error.Conflict(description: $"User with email '{request.Email}' exist");        
        }

        var user = new User(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        await usersRepository.AddAsync(user,cancellationToken);

        return Result.Success;
    }
}
