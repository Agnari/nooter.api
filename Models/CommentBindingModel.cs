namespace Nooter.API.Models
{
    public class CommentBindingModel
    {
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
        public string CommenterId { get; set; }
    }
}
