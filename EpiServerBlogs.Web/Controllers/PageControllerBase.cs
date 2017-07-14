using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Partial;
using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Mvc;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Controllers
{
    public class PageControllerBase<T> : PageController<T> where T : SitePageData
    {
        private readonly ISiteCartService _siteCartService;

        public PageControllerBase(ISiteCartService siteCartService)
        {
            _siteCartService = siteCartService;
        }

        public ActionResult TopLinksPartial(T currentPage)
        {
            var user = User.Identity;
            var loginPage = DataFactory.Instance.GetChildren<LoginPage>(ContentReference.StartPage).FirstOrDefault();
            var accountPage = DataFactory.Instance.GetChildren<AccountPage>(ContentReference.StartPage).FirstOrDefault();

            return PartialView("PagePartials/TopLinksPartial", new TopLinksPartialViewModel
            {
                IsAuthenticated = user.IsAuthenticated,
                UserName = user.Name,
                ReturnUrl = Request.RawUrl,
                Language = ContentLanguage.PreferredCulture.Name,
                LoginPageLink = loginPage == null ? PageReference.EmptyReference : loginPage.PageLink,
                IsLoginPage = currentPage is LoginPage,
                AccountPageLink = accountPage == null ? PageReference.EmptyReference : accountPage.PageLink
            });
        }

        public ActionResult ShoppingCartPartial()
        {
            var checkoutPage = DataFactory.Instance.GetChildren<CheckoutPage>(ContentReference.StartPage).FirstOrDefault();

            var cart = (Cart)_siteCartService.GetCart(_siteCartService.DefaultCartName);
            return PartialView("PagePartials/ShoppingCartPartial", new ShoppingCartPartialViewModel
            {
                CartName = cart.Name,
                TotalCount = _siteCartService.GetTotalCartQuantity(cart.Name),
                CheckoutPageLink = checkoutPage == null ? PageReference.EmptyReference : checkoutPage.PageLink
            });
        }
    }
}