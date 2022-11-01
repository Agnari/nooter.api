using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nooter.API.Models;

namespace Nooter.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentityDbContext _db;
        public UsersController(
           UserManager<AppUser> userManager, AppIdentityDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result);


            return Ok(user);
        }
    }
}
