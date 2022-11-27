using Identity.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Nooter.API.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
