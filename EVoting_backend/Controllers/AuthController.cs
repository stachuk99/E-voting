﻿using EVoting_backend.API.Request;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using EVoting_backend.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EVoting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly AppDbContext _dbContext;
        private readonly TokenManager _tokenManager;
        private readonly TokenGenerator _tokenGenerator;
        private readonly Authenticator _authenticator;

        public AuthController(UserManager userManager, AppDbContext dbContext, TokenManager tokenManager, TokenGenerator tokenGenerator, Authenticator authenticator)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _tokenManager = tokenManager;
            _tokenGenerator = tokenGenerator;
            _authenticator = authenticator;
        }

        [HttpPost("google-request")]
        public async Task<IActionResult> GoogleAuthenticationRequets(GoogleLoginRequest token)
        {
            var validPayLoad = await GoogleJsonWebSignature.ValidateAsync(token.IdToken);
            User user = null;
            user = await _userManager.GetUserByEmail(validPayLoad.Email);
            if (user == null)
            {
                user = new User();
                user.Email = validPayLoad.Email;
                var result = await _userManager.AddUser(user);
                if (result)
                {
                    return Ok(await _authenticator.Authenticate(user));
                }
                else
                    return Conflict(result.ToString());
            }
            else
            {
                return Ok(await _authenticator.Authenticate(user));
            }
        }
    }
}
