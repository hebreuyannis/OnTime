﻿using OnTime.Domain.User;

namespace OnTime.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByCredentialAsync(string email, string password, CancellationToken cancellationToken);
    Task<List<User>> GetAllUserAsync(CancellationToken cancellationToken);
}