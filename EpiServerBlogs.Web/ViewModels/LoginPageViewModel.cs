using EpiServerBlogs.Web.Models.Pages;

namespace EpiServerBlogs.Web.ViewModels
{
    public class LoginPageViewModel : SitePageViewModel<LoginPage>
    {
        public LoginPageViewModel(LoginPage currentPage) : base(currentPage)
        {
        }
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool ShowErrorMessage { get; set; }

        public string ErrorMessage { get; set; }
    }
}