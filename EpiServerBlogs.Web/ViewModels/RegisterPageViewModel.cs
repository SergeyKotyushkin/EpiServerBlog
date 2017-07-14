using EpiServerBlogs.Web.Models.Pages;
using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels
{
    public class RegisterPageViewModel : SitePageViewModel<RegisterPage>
    {
        public RegisterPageViewModel(RegisterPage currentPage) : base(currentPage)
        {
            ThisPageLink = currentPage.PageLink;
        }

        public ContentReference ThisPageLink { get; private set; }

        public RegisterPagePostViewModel RegisterPagePostViewModel { get; set; }

        public bool ShowErrorMessage { get; set; }

        public string ErrorMessage { get; set; }
    }
}