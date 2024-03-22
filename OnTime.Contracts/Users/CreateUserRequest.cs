namespace OnTime.Contracts.Users;

public record CreateUserRequest(string Firstname, string Lastname, string Email, string Password)
{
}
