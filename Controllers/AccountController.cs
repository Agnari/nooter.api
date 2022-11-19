using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nooter.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdmissionTool.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return BadRequest("Incorrect Email or Password");

            var token = await GetToken(user);


            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id.ToString(),
                Expiration = token.ValidTo,
                UserName = user.UserName,
            });
        }

        private async Task<JwtSecurityToken> GetToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTkey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: null,
                audience: null,
                expires: DateTime.Now.AddHours(6),
                claims: claims,
                signingCredentials: credentials
                );
        }
    }
}
