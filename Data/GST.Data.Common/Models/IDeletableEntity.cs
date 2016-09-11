namespace GST.Data.Common.Models
{
    using System;

    public interface IDeletableEntity
    {
        bool isDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
