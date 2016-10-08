namespace GST.Web.ViewModels.LogsViewModels
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class LogsViewModel : IMapFrom<Log>
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
