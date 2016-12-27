using System.Linq;
using EpiServerBlogs.Web.Models.Pages;
using EPiServer;
using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels
{
    public class LoginPageViewModel : SitePageViewModel<LoginPage>
    {
        public LoginPageViewModel(LoginPage currentPage) : base(currentPage)
        {
            var registerPage =
                DataFactory.Instance.GetChildren<RegisterPage>(ContentReference.StartPage).FirstOrDefault();

            RegisterPageLink = registerPage == null ? PageReference.EmptyReference : registerPage.PageLink;
        }
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool ShowErrorMessage { get; set; }

        public string ErrorMessage { get; set; }

        public PageReference RegisterPageLink { get; private set; }
    }
}