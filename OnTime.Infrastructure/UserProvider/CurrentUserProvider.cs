using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnTime.Infrastructure.UserProvider;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        if(_httpContextAccessor.HttpContext is null)
             throw new ArgumentNullException(nameof(HttpContext));

        var id = Guid.Parse(GetSingleClaimValue("id"));
        var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues(ClaimTypes.Role);
        var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        var lastName = GetSingleClaimValue(ClaimTypes.Surname);
        var email = GetSingleClaimValue(ClaimTypes.Email);

        return new CurrentUser(id, firstName, lastName, email, permissions, roles);
    }

    public string GetCurrentUserId() => GetSingleClaimValue("id");

    private List<string> GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}
