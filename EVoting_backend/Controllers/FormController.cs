using EVoting_backend.API.Request;
using EVoting_backend.DB.Models;
using EVoting_backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EVoting_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {

        private readonly UserManager _userManager;
        private readonly FormManager _formManager;

        public FormController(UserManager userManager, FormManager formManager)
        {
            _userManager = userManager;
            _formManager = formManager;
        }

        [HttpPost("definition")]
        //[Authorize(AuthenticationSchemes = "OAuth")]
        public async Task<IActionResult> PostForm(PostFormRequest formRequest)
        {
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //var requestUserMail = identity.FindFirst(ClaimTypes.Email)?.Value;
            //User user = await _userManager.GetUserByEmail(requestUserMail);
            //if (user.Token != accessToken) return Unauthorized();
            
            if(await _formManager.AddForm(formRequest))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Wrong scheme");
            }


            //await _userManager.ReleaseToken(user.Email);
        }

    }
}
