namespace OnTime.Application.Users.Queries.Token;

public record GenerateTokenResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
