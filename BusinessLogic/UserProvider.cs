using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

public interface IUserProvider {
    User GetAuthorizedUser();
}

public class UserProvider : IUserProvider
{
    private readonly IRepository<User> userRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserProvider(IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
    {
        this.userRepository = userRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public User GetAuthorizedUser()
    {
        var contextUser = httpContextAccessor.HttpContext.User;
        var userId = int.Parse(contextUser.Claims.First(u => u.Type == JwtRegisteredClaimNames.Sid).Value);
        var user = userRepository.Get(userId);
        return user;
    }
}