namespace GST.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System;

    public class Picture : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool isDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
