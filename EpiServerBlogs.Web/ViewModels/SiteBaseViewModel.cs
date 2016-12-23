using System.Security.Principal;
using EpiServerBlogs.Web.Models.Pages;

namespace EpiServerBlogs.Web.ViewModels
{
    public class SiteBaseViewModel<T> where T: SitePageData
    {
        public T CurrentPage { get; set; }

        public LoginPage LoginPage { get; set; }

        public IIdentity User { get; set; }

        public bool IsLoginPage { get; set; }
    }
}