namespace GST.Data.Models.Base
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public abstract class Media : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }
    }
}
