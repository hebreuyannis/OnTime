namespace OnTime.Contracts.Users
{
    public record UserAuthenticateRequest(string email, string password)
    {
    }
}
