using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

public interface IAuthorizationService
{
    TokenResponse GenerateTokens(User user);
    TokenResponse RefreshToken(User user, string refreshToken);
}

public class AuthorizationService : IAuthorizationService
{
    private readonly IOptions<TokenConfig> _tokenConfig;

    private readonly IRepository<UserSessions> _userSessionRepository;

    public AuthorizationService(IOptions<TokenConfig> tokenConfig, IRepository<UserSessions> userSessionRepository){
        _tokenConfig = tokenConfig;
        _userSessionRepository = userSessionRepository;
    }

    public TokenResponse GenerateTokens(User user)
    {
        CleanRefreshTokensFor(user.Id);

        var refreshToken = GenerateToken(user, _tokenConfig.Value.RefreshExpireMinutes);

        StoreRefreshToken(user.Id, refreshToken);

        return new TokenResponse(){
            AccessToken = GenerateToken(user, _tokenConfig.Value.AccessExpireMinutes),
            RefreshToken = refreshToken
        };
    }

    public TokenResponse RefreshToken(User user, string refreshToken)
    {
        var userSession = _userSessionRepository
                                .FindBy(r => r.RefreshToken == refreshToken && r.UserId == user.Id)
                                .FirstOrDefault();

        if (userSession == null)
        {
            return null;
        }

        if(userSession.Expire < DateTime.UtcNow){
            CleanRefreshTokensFor(user.Id);
            return null;
        }

        return GenerateTokens(user);
    }

    private void CleanRefreshTokensFor(int userId)
    {
        var refreshTokens = _userSessionRepository.FindBy(r => r.UserId == userId);

        foreach(var refreshToken in refreshTokens){
            _userSessionRepository.Delete(refreshToken);
        }
    }

    private void StoreRefreshToken(int userId, Token refreshToken)
    {
        _userSessionRepository.Add(new UserSessions(){
            RefreshToken = refreshToken.Value,
            UserId = userId,
            Expire = DateTime.UtcNow.AddMinutes(refreshToken.Expiry)
        });
    }
    private Token GenerateToken(User user, int expiryTime)
    {
        var now = DateTime.UtcNow;
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString())
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.Key))
            , SecurityAlgorithms.HmacSha256);

        var expiry = now.AddMinutes(expiryTime);

        var jwt = new JwtSecurityToken(
            claims: claims,
            notBefore: now,
            expires: expiry,
            signingCredentials: signingCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new Token()
        {
            Value = token,
            Expiry = expiryTime
        };
    }
}