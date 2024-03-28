﻿namespace OnTime.Infrastructure.UserProvider;

public record CurrentUser(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles);
