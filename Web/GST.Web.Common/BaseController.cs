namespace GST.Web.Common
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        protected int MaxMediaPerPage
        {
            get
            {
                return GlobalConstants.MaxMediaPerPage;
            }
        }

        protected int MaxPostsPerPage
        {
            get
            {
                return GlobalConstants.MaxPostsPerPage;
            }
        }

        #region Helpers
        protected int GetLinksCountFor(int totalItems)
        {
            return (int)Math.Ceiling(totalItems / (float)GlobalConstants.MaxMediaPerPage);
        }

        protected int PageChecks(int? page, string RedirectActionName)
        {
            if (page == null)
            {
                return 1;
            }
            else if (page <= 0)
            {
                RedirectToAction(RedirectActionName, new { p = 1 });
                return 0;
            }
            else
            {
                return (int)page;
            }
        }

        protected int GetPaginationDataToSkip(int page)
        {
            return (page * MaxMediaPerPage) - MaxMediaPerPage;
        }
        #endregion
    }
}
