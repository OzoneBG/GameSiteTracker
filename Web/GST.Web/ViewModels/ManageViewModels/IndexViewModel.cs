namespace GST.Web.ViewModels.ManageViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
