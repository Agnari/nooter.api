using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nooter.API.Models;

namespace Nooter.API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly AppIdentityDbContext _db;

        public CommentController(AppIdentityDbContext duomb)
        {
            _db = duomb;
        }

        [HttpGet(template: "{id}")]
        public IActionResult Get(Guid id)
        {
            var comment = _db.Comments.Select(x => new
            {
                Id = x.Id,
                Text = x.Text,
                ArticleId = x.ArticleId,
                CommenterName = x.Commenter.UserName
            }).Where(x => x.ArticleId == id);
            return Ok(comment);
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentBindingModel model)
        {
            var comment = new Comment()
            {
                Id = Guid.NewGuid(),
                Text = model.Text,
                ArticleId = model.ArticleId,
                CommenterId = model.CommenterId,
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return Ok();
        }
    }
}
