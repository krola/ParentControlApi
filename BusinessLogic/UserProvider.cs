using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

public interface IUserProvider {
    string GetAuthorizedUserId();
}

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string GetAuthorizedUserId()
    {
        var contextUser = httpContextAccessor.HttpContext.User;
        return contextUser.Claims.First(u => u.Type == "uid").Value;
    }
}