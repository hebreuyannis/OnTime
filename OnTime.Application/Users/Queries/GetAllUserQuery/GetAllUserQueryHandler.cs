using MediatR;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.Common.Error;
using OnTime.Domain.User;

namespace OnTime.Application.Users.Queries.GetAllUserQuery;

public class GetAllUserQueryHandler(IUsersRepository _usersRepository) : IRequestHandler<GetAllUserQuery, ErrorOr<List<User>>>
{
    public async Task<ErrorOr<List<User>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
		try
		{
			return await _usersRepository.GetAllUserAsync(cancellationToken);
        }
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message);
		} 
    }
}
