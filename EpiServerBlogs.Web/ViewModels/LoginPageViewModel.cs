using System.Security.Principal;
using EpiServerBlogs.Web.Models.Pages;

namespace EpiServerBlogs.Web.ViewModels
{
    public class LoginPageViewModel : SitePageViewModel<LoginPage>
    {
        public LoginPageViewModel(LoginPage currentPage, IIdentity user) : base(currentPage, user)
        {
            IsLoginPage = true;
        }

        public LoginPageViewModel(SiteBaseViewModel<LoginPage> model) : base(model)
        {
            IsLoginPage = true;
        }
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool ShowErrorMessage { get; set; }

        public string ErrorMessage { get; set; }
    }
}