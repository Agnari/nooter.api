using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Nooter.API.Models;

namespace Nooter.API.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        // GET api/<ArticlesController>

        private readonly AppIdentityDbContext _duomb;

        public ArticlesController(AppIdentityDbContext duomb)
        {
            _duomb = duomb;
        }

        private static IEnumerable<Article> List()
        {
            var articles = new List<Article>();
            return articles;
        }

        [HttpGet]
        public IEnumerable<Article> Get()
        {

            return _duomb.Articles.ToList();
        }

        // GET api/<ArticlesController>/5
        [HttpGet(template: "{id}")]
        public Article Get(Guid id)
        {
            var articles = List();
            var article = _duomb.Articles.FirstOrDefault(x => x.Id == id);
            return article;
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public void Post([FromBody] Article model)
        {
            model.Id = Guid.NewGuid();
            _duomb.Articles.Add(model);
            _duomb.SaveChanges();
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Article model)
        {
            var artik = _duomb.Articles.FirstOrDefault(x => x.Id == id);
            if (artik != null)
            {
                artik.Id = id;
                artik.Author = model.Author;
                artik.Title = model.Title;
                artik.Body = model.Body;

                _duomb.SaveChanges();
            }
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var article1 = _duomb.Articles.FirstOrDefault(x => x.Id == id);

            _duomb.Articles.Remove(article1);
            _duomb.SaveChanges();
        }
    }
}
