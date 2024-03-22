using Microsoft.EntityFrameworkCore;
using OnTime.Application.Common.Interfaces;
using OnTime.Domain.User;
using OnTime.Infrastructure.Common.Persistence;

namespace OnTime.Infrastructure.Users.Persistence;

public class UsersRepository(OnTimeDbContext _dbContext) : IUsersRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
       await _dbContext.AddAsync(user, cancellationToken);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        await _dbContext.Appointments.LoadAsync();
        return await _dbContext.Users.FindAsync(userId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
       return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email),cancellationToken);
    }
}
