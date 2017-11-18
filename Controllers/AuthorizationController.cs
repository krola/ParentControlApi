using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
    private readonly IOptions<TokenAuthentication> _tokenConfig;

    public AuthorizationController(IOptions<TokenAuthentication> tokenConfig) {
        this._tokenConfig = tokenConfig;
    }

        [HttpPost]
        public IActionResult GetToken([FromBody]LogInDTO authorizationData)
        {
            if (authorizationData.UserName != "test")
                return Unauthorized();

            var token = GenerateToken(new User() { Id = 1, Name = "test" }, DateTime.Now.AddHours(1));

            return Ok(token);
        }

        private string GenerateToken(User user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Name, "TokenAuth"),
                new[] {
                    new Claim("ID", user.Id.ToString())
                }
            );

            var secretKey = Encoding.ASCII.GetBytes(_tokenConfig.Value.SecretKey);
            var signingKey = new SymmetricSecurityKey(secretKey);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = expires,
                NotBefore = DateTime.Now.Subtract(TimeSpan.FromMinutes(30))
            });
            return handler.WriteToken(securityToken);
        }
    }
