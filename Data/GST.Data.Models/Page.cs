namespace GST.Data.Models
{
    using Base.Interfaces;
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Page : AuditInfo, IEntityOwner
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }

        public string AuthorId { get; set; }
    }
}
