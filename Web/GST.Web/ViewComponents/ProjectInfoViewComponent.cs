namespace GST.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using ViewModels.ViewComponentViewModels;

    public class ProjectInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            string path = Directory.GetCurrentDirectory() + "\\progressdata.json";

            string json = File.ReadAllText(path);

            var model = JsonConvert.DeserializeObject<ProjectInfoViewModel>(json);

            return View(model);
        }
    }
}
