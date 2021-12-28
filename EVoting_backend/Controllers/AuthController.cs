using EVoting_backend.API.Request;
using EVoting_backend.API.Response;
using EVoting_backend.DB;
using EVoting_backend.DB.Models;
using EVoting_backend.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public async Task<IActionResult> GoogleAuthenticationRequets(GoogleLoginRequest googleToken)
        {
            var validPayLoad = await GoogleJsonWebSignature.ValidateAsync(googleToken.IdToken);
            //var validClientKey = await GoogleJsonWebSignature.ValidateAsync(googleToken.PublicKey);
            User user = null;
            user = await _userManager.GetUserByEmail(validPayLoad.Email);
            string sharedKey = "";
            string ServerPublicKey = "";
            //using (ECDiffieHellman server = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256))
            //{
            //    ServerPublicKey = Convert.ToBase64String(server.ExportSubjectPublicKeyInfo());
            //    Console.WriteLine("Alice's public:        " + ServerPublicKey);
            //    Console.WriteLine("Alice's private:       " + Convert.ToBase64String(server.ExportPkcs8PrivateKey()));
            //    ECDiffieHellman bob = ECDiffieHellman.Create();
            //    bob.ImportSubjectPublicKeyInfo(Convert.FromBase64String(JsonConvert.ToString(validClientKey)), out _);
            //    byte[] sharedSecret = server.DeriveKeyMaterial(bob.PublicKey);
            //    sharedKey = Convert.ToBase64String(sharedSecret);
            //}
            if (user == null)
            {
                user = new User();
                user.Email = validPayLoad.Email;
                user.Secret = sharedKey;
                var result = await _userManager.AddUser(user);
                if (result)
                {
                    var tokenReponse = await loginUser(user, ServerPublicKey);
                    if (tokenReponse != null)
                        return Ok(tokenReponse);
                    else
                        return BadRequest("Not logged in");
                }
                else
                    return Conflict(result.ToString());
            }
            else
            {
                var tokenReponse = await loginUser(user, ServerPublicKey);
                if (tokenReponse != null)
                    return Ok(tokenReponse);
                else
                    return BadRequest("Not logged in");
            }
        }


        [HttpDelete("logout")]
        [Authorize(AuthenticationSchemes = "OAuth")]
        public async Task<IActionResult> LogoutUser()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var requestUserMail = identity.FindFirst(ClaimTypes.Email)?.Value;
            User user = await _userManager.GetUserByEmail(requestUserMail);
            await _userManager.ReleaseToken(user.Email);
            return NoContent();
        }

        private async Task<AuthenticatedResponse> loginUser(User user, string key)
        {
            AuthenticatedResponse tokenResponse = await _authenticator.Authenticate(user, key);
            if (tokenResponse != null)
            {
                var loginResult = await _userManager.SetToken(user.Email, tokenResponse.AccessToken);
                if (loginResult)
                    return tokenResponse;
                else return null;
            }
            return null;
        }
    }
}
