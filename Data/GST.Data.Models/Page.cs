namespace GST.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Page : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
