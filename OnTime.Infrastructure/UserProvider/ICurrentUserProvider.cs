namespace OnTime.Infrastructure.UserProvider;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();

    string GetCurrentUserId();
}
