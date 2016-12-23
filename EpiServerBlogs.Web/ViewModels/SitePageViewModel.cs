using System.Security.Principal;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Contracts;

namespace EpiServerBlogs.Web.ViewModels
{
    public class SitePageViewModel<T> : ISitePageViewModel<T> where T : SitePageData
    {
        public SitePageViewModel(T currentPage, IIdentity user)
        {
            CurrentPage = currentPage;

            IsAuthenticated = user.IsAuthenticated;
            CurrentUserName = user.Name;
        }
        
        public SitePageViewModel(SiteBaseViewModel<T> model)
        {
            CurrentPage = model.CurrentPage;

            IsAuthenticated = model.User.IsAuthenticated;
            CurrentUserName = model.User.Name;
            LoginPage = model.LoginPage;
        }

        public T CurrentPage { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string CurrentUserName { get; private set; }

        public LoginPage LoginPage { get; private set; }

        public bool IsLoginPage { get; set; }
    }

    public static class SitePageViewModel
    {
        /// <summary>
        /// Returns a SitePageViewModel of type <typeparam name="T"/>.
        /// </summary>
        /// <remarks>
        /// Convenience method for creating SitePageViewModels without having to specify the type as methods can use type inference while constructors cannot.
        /// </remarks>
        public static SitePageViewModel<T> Create<T>(T page, IIdentity user) where T : SitePageData
        {
            return new SitePageViewModel<T>(page, user);
        }
        
        public static SitePageViewModel<T> Create<T>(SiteBaseViewModel<T> model) where T : SitePageData
        {
            return new SitePageViewModel<T>(model);
        }
    }
}