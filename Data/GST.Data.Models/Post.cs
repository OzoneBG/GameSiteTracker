namespace GST.Data.Models
{
    using Base.Interfaces;
    using Common.Models;

    public class Post : AuditInfo, IEntityOwner
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }
    }
}
