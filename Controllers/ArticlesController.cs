using Microsoft.AspNetCore.Mvc;
using Nooter.API.Models;

namespace Nooter.API.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly AppIdentityDbContext _db;

        public ArticlesController(AppIdentityDbContext db)
        {
            _db = db;
        }

        private static IEnumerable<Article> List()
        {
            var articles = new List<Article>();
            var article1 = new Article()
            {
                Id = new Guid("5bafeb38-feed-4410-0001-ade4cebc6f16"),
                Title = "Title #1",
                Body =
                    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            };
            var article2 = new Article()
            {
                Id = new Guid("5bafeb38-feed-4410-0002-ade4cebc6f16"),
                Title = "Title #2",
                Body =
                    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            };
            articles.Add(article1);
            articles.Add(article2);
            return articles;
        }

        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return _db.Articles.ToList();
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public Article Get(Guid id)
        {
            var article = _db.Articles.FirstOrDefault(x => x.Id == id);
            return article;
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public void Post([FromBody] Article model)
        {
            model.Id = Guid.NewGuid();
            _db.Articles.Add(model);
            _db.SaveChanges();

        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
