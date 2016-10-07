namespace GST.Web.Controllers
{
    using Common;
    using Common.Extensions;
    using Common.Mapping;
    using Data.Models;
    using Data.Services.Interfaces;
    using InputModels.PagesInputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.AccountViewModels;
    using ViewModels.PagesViewModel;
    using ViewModels.VideosViewModels;
    using InputModels.VideosInputModels;
    using InputModels.PicturesInputModels;
    using ViewModels.PicturesViewModels;
    using System.IO;
    using Newtonsoft.Json;
    using ViewModels;
    using ViewModels.PostsViewModels;
    using InputModels.PostsInputModels;

    public abstract class ModeratorController : BaseController
    {
        //Services
        protected readonly IPagesService pageService;
        protected readonly IUsersService usersService;
        protected readonly ILogService logsService;
        protected readonly IVideosService videosService;
        protected readonly IPicturesService picturesService;
        protected readonly IPostsService postsService;

        //Managers
        protected readonly UserManager<User> userManager;

        public ModeratorController(IPagesService pageService,
            IUsersService usersService,
            ILogService logsService,
            IVideosService videosService,
            IPicturesService picturesService,
            IPostsService postsService,
            UserManager<User> userManager)
        {
            this.pageService = pageService;
            this.usersService = usersService;
            this.logsService = logsService;
            this.videosService = videosService;
            this.picturesService = picturesService;
            this.postsService = postsService;

            this.userManager = userManager;
            
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult AllUsers()
        {
            var allUsers = usersService.GetAllUsers().To<ViewUsersViewModel>().ToList();

            return View(allUsers);
        }

        #region Posts
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult ListPosts(int? p)
        {
            int page = PageChecks(p, "ListPosts");

            var allPosts = postsService.GetAllPosts();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allPosts.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var postsList = allPosts.Skip(toSkip).Take(GlobalConstants.MaxMediaForAdmin).To<ListPostsViewModel>().ToList();

            return View(postsList);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(NewPostInputModel model)
        {
            if (ModelState.IsValid)
            {
                postsService.AddPost(model.Title, model.Content, GetCurrentUser(User.GetUserId()).Result.Id);

                string contents = string.Format("{0} added new post {1}", GetCurrentUserName(User.GetUserId()), model.Title);
                logsService.AddNewLog("Create", contents);

                return RedirectToAction("ListPosts");
            }
            else
            {
                return View();
            }
        }


        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            var post = postsService.GetPostWithId(id).To<EditPostViewModel>().FirstOrDefault();

            return View(post);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(EditPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                postsService.EditPost(model.Id, model.Title, model.Content);

                string content = string.Format("{0} edited post {1}", GetCurrentUserName(User.GetUserId()), model.Title);
                logsService.AddNewLog("Create", content);

                return RedirectToAction("EditPost", new { id = model.Id });
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult DeletePost(int id)
        {
            string postTitle = postsService.GetPostWithId(id).FirstOrDefault().Title;
            string content = string.Format("{0} deleted post {1}", GetCurrentUserName(User.GetUserId()), postTitle);
            logsService.AddNewLog("Delete", content);

            postsService.DeletePost(id);

            return RedirectToAction("ListPosts");
        }

        #endregion

        #region Game Information
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditGameInfo()
        {
            string json = System.IO.File.ReadAllText("progressdata.json");
            var gameInfo = JsonConvert.DeserializeObject<ProgressDataViewModel>(json);

            return View(gameInfo);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGameInfo(ProgressDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                string json = JsonConvert.SerializeObject(model);
                System.IO.File.WriteAllText("progressdata.json", json);

                return RedirectToAction("EditGameInfo");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region Pictures
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult ViewPictures(int? p)
        {
            int page = PageChecks(p, "ViewPictures");

            var allPictures = picturesService.GetAllPictures();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allPictures.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var picturesList = allPictures.Skip(toSkip).Take(MaxMediaPerPage).To<AdministrationPicturesViewModel>().ToList();

            return View(picturesList);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult CreatePicture()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePicture(NewPictureInputModel model)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                if (model.File.Length > 0)
                {
                    var path = Path.Combine(uploads, model.File.FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        //Create file
                        model.File.CopyTo(fileStream);

                        picturesService.AddPicture(model.Name, model.File.FileName);

                        string content = string.Format("{0} uploaded photo: {1}", GetCurrentUserName(User.GetUserId()), model.Name);
                        logsService.AddNewLog("Create", content);

                        return RedirectToAction("ViewPictures");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult DeletePicture(int id)
        {
            string picName = picturesService.GetAllPictures().Where(x => x.Id == id).FirstOrDefault().Name;

            string content = string.Format("{0} deleted {1}", GetCurrentUserName(User.GetUserId()), picName);

            logsService.AddNewLog("Delete", content);

            picturesService.DeletePicture(id);

            return RedirectToAction("ViewPictures");
        }
        #endregion

        #region Videos
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult DeleteVideo(int id)
        {
            string userName = GetCurrentUserName(User.GetUserId());
            string videoName = videosService.GetVideoName(id);

            string content = string.Format("{0} requested deletion of video: {1}", userName, videoName);
            logsService.AddNewLog("Delete", content);

            videosService.DeleteVideo(id);

            return RedirectToAction("ViewVideos");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult CreateVideo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult CreateVideo(NewVideoInputModel model)
        {
            if (ModelState.IsValid)
            {
                videosService.AddNewVideo(model.Name, model.VideoUrl);

                string content = string.Format("{0} added video: {1}", GetCurrentUserName(User.GetUserId()), model.Name);
                logsService.AddNewLog("Create", content);

                return RedirectToAction("ViewVideos");
            }
            else
            {
                return View();
            } 
        }

        public IActionResult ViewVideos(int? p)
        {
            int page = PageChecks(p, "Videos");

            var allVideos = videosService.GetAllVideos();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allVideos.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var videosList = allVideos.Skip(toSkip).Take(MaxMediaPerPage).To<AdministrationVideosViewModel>().ToList();

            return View(videosList);
        }
        #endregion       

        #region Static Pages
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditStaticPages(string pageName)
        {
            if (string.IsNullOrWhiteSpace(pageName))
            {
                pageName = pageService.GetFirstPageName();
            }

            var pageData = PopulateAdminPageViewModel(pageName);

            return View(pageData);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStaticPages(EditPageInputModel model)
        {
            if (ModelState.IsValid)
            {
                pageService.EditStaticPage(model.OldName, model.Name, model.Content);

                string content = string.Format("{0} modified contents of pagename: {1} / {2}.", GetCurrentUserName(User.GetUserId()), model.OldName, model.Name);
                logsService.AddNewLog("Modify", content);

                return RedirectToAction("EditStaticPages", new { pageName = model.Name });
            }
            else
            {
                var pageData = PopulateAdminPageViewModel(model.Name);

                pageData.Content = "";

                ViewData["message"] = "Content cannot be empty!";

                return View(pageData);
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult NewPage()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPage(NewPageInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetCurrentUser(User.GetUserId()).Result;

                pageService.CreateNewPage(model.Name, model.Content, user);

                //Log
                //[UserName] created new page: {0}
                string content = string.Format("{0} created new page: {1}", GetCurrentUserName(User.GetUserId()), model.Name);
                logsService.AddNewLog("Create", content);

                return RedirectToAction("EditStaticPages", new { pageName = model.Name });
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeletePage(int pageId)
        {
            string pageName = pageService.GetPageNameById(pageId);

            pageService.DeletePage(pageId);

            string content = string.Format("{0} requested deletion of page: {1}", GetCurrentUserName(User.GetUserId()), pageName);
            logsService.AddNewLog("Delete", content);

            return RedirectToAction("EditStaticPages");
        }
        #endregion

        #region Helpers
        protected AdministrationPageViewModel PopulateAdminPageViewModel(string pageName)
        {
            var pageData = pageService.GetPageFor(pageName).To<AdministrationPageViewModel>().FirstOrDefault();

            var pageNames = pageService.GetAllPageNames();

            pageData.PageNames = pageNames;

            return pageData;
        }

        protected Task<User> GetCurrentUser(string id) => userManager.FindByIdAsync(id);

        protected string GetCurrentUserName(string id)
        {
            return GetCurrentUser(id).Result.UserName;
        }
        #endregion
    }
}
