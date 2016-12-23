using System.Linq;
using System.Security.Principal;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;

namespace EpiServerBlogs.Web.Business.Helpers
{
    public class SiteBaseHelper
    {
        public static SiteBaseViewModel<T> CreateViewModel<T>(IIdentity user, T currentPage) where T : SitePageData
        {
            var loginPage = DataFactory.Instance.GetChildren<LoginPage>(ContentReference.StartPage).FirstOrDefault();

            return new SiteBaseViewModel<T>
            {
                CurrentPage = currentPage,
                LoginPage = loginPage,
                User = user
            };
        }
    }
}