using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParentControlApi.DTO;

[Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
    private readonly IAuthorizationService _authorizationService;
    private readonly IRepository<User> _userRepository;
    private readonly IUserProvider _userProvider;

    public AuthorizationController(IAuthorizationService authorizationService, 
    IRepository<User> userRepository,
    IUserProvider userProvider) {
        _authorizationService = authorizationService;
        _userRepository = userRepository;
        _userProvider = userProvider;
    }

        [HttpPost]
        [Route("Token")]
        public IActionResult GetToken([FromBody]LogInDTO authorizationData)
        {
            var user = _userRepository.FindBy(u => u.Name == authorizationData.Username && 
            u.Password == authorizationData.Password).SingleOrDefault();
            if (user == null)
                return Unauthorized();

            return Ok(_authorizationService.GenerateTokens(user));
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken([FromBody]RefreshTokenDTO refreshToken)
        {
            var user = _userProvider.GetAuthorizedUser();
            if (user == null)
                return Unauthorized();

            var tokens = _authorizationService.RefreshToken(user, refreshToken.RefreshToken);
            return Ok(tokens);
        }
    }
