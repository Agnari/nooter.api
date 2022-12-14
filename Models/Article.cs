using Identity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Nooter.API.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string ImageURL { get; set; }
        public string AuthorId { get; set; }
        public virtual AppUser Author { get; set; }
    }
}
