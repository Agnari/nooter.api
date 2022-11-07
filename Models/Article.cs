using System.Reflection;

namespace Nooter.API.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
