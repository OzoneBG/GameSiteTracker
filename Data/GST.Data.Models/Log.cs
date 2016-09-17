namespace GST.Data.Models
{
    using Common.Models;
    using System;

    public class Log : AuditInfo
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }
    }
}
