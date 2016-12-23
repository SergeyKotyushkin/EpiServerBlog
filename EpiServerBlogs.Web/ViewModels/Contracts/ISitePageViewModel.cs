using EpiServerBlogs.Web.Models.Pages;

namespace EpiServerBlogs.Web.ViewModels.Contracts
{
    public interface ISitePageViewModel<out T> where T : SitePageData
    {
        T CurrentPage { get; }

        bool IsAuthenticated { get; }

        string CurrentUserName { get; }

        LoginPage LoginPage { get; }

        bool IsLoginPage { get; set; }
    }
}