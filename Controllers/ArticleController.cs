using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nooter.API.Models;
using System.Security.Claims;

namespace Nooter.API.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        // GET api/<ArticlesController>

        private readonly AppIdentityDbContext _db;

        public ArticlesController(AppIdentityDbContext duomb)
        {
            _db = duomb;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var list = _db.Articles.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Body,
                ImageURL = x.ImageURL,
                AuthorName = x.Author.UserName
            });
            return Ok(list);
        }

        // GET api/<ArticlesController>/5
        [HttpGet(template: "{id}")]
        public IActionResult Get(Guid id)
        {
            var article = _db.Articles.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Body,
                ImageURL = x.ImageURL,
                AuthorName = x.Author.UserName
            }).FirstOrDefault(x => x.Id == id);
            return Ok(article);
        }

        // POST api/<ArticlesController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ArticleBindingModel model)
        {
            var userIdClaim = this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            var article = new Article()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Body = model.Body,
                ImageURL = model.ImageURL,
                AuthorId = userIdClaim.Value,
            };
            _db.Articles.Add(article);
            await _db.SaveChangesAsync();
            return Ok(article);
        }

        [HttpGet(template: "{id}/UsersAllArticles")]
        public IActionResult GetAuthorArticles(Guid id)
        {
            var article = _db.Articles.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Body,
                ImageURL = x.ImageURL,
                AuthorName = x.Author.UserName,
                AuthorId = x.AuthorId
            }).Where(x => x.AuthorId == id.ToString());
            return Ok(article);
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ArticleBindingModel model)
        {
            var article = _db.Articles.FirstOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.Id = id;
                article.Title = model.Title;
                article.Body = model.Body;
                article.ImageURL = model.ImageURL;
                _db.SaveChanges();
            }
            return Ok();
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var article1 = _db.Articles.FirstOrDefault(x => x.Id == id);

            _db.Articles.Remove(article1);
            _db.SaveChanges();
        }
    }
}
