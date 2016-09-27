namespace GST.Web.ViewModels.PagesViewModel
{
    using Data.Models;
    using Common.Mapping;
    using System;
    using System.Collections.Generic;

    public class AdministrationPageViewModel : IMapFrom<Page>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public List<string> PageNames { get; set; }
    }
}
