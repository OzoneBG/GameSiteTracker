namespace GST.Data.Models
{
    using Common.Models;

    public class Post : AuditInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }
    }
}
