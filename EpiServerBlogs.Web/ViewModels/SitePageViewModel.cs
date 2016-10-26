using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Contracts;

namespace EpiServerBlogs.Web.ViewModels
{
    public class SitePageViewModel<T> : ISitePageViewModel<T> where T : SitePageData
    {
        public SitePageViewModel(T currentPage)
        {
            CurrentPage = currentPage;
        }

        public T CurrentPage { get; private set; }
    }

    public static class SitePageViewModel
    {
        /// <summary>
        /// Returns a SitePageViewModel of type <typeparam name="T"/>.
        /// </summary>
        /// <remarks>
        /// Convenience method for creating SitePageViewModels without having to specify the type as methods can use type inference while constructors cannot.
        /// </remarks>
        public static SitePageViewModel<T> Create<T>(T page) where T : SitePageData
        {
            return new SitePageViewModel<T>(page);
        }
    }
}