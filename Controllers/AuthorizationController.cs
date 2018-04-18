using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParentControlApi.DTO;

[Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
    private readonly IOptions<TokenAuthentication> _tokenConfig;
    private readonly IRepository<User> userRepository;

    public AuthorizationController(IOptions<TokenAuthentication> tokenConfig, IRepository<User> userRepository) {
        this._tokenConfig = tokenConfig;
        this.userRepository = userRepository;
    }

        [HttpPost]
        public IActionResult GetToken([FromBody]LogInDTO authorizationData)
        {
            var user = userRepository.FindBy(u => u.Name == authorizationData.UserName && 
            u.Password == authorizationData.Password).SingleOrDefault();
            if (user == null)
                return Unauthorized();

            var expires = new TimeSpan(0,60,0);
            var token = GenerateToken(user, expires);

            return Ok(new Authorization(){
                Token = token,
                Expires = expires.TotalMinutes
            });
        }

        private string GenerateToken(User user, TimeSpan expiresTime)
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
                Expires = DateTime.Now.AddMinutes(expiresTime.TotalMinutes),
                NotBefore = DateTime.Now.Subtract(TimeSpan.FromMinutes(30))
            });
            return handler.WriteToken(securityToken);
        }
    }
