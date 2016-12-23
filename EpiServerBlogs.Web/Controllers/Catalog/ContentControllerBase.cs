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

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class ContentControllerBase<T> : ContentController<T> where T : IContent
    {
        private readonly ISiteCartService _siteCartService;

        public ContentControllerBase(ISiteCartService siteCartService)
        {
            _siteCartService = siteCartService;
        }

        public ActionResult TopLinksPartial()
        {
            var user = User.Identity;
            var loginPage = DataFactory.Instance.GetChildren<LoginPage>(ContentReference.StartPage).FirstOrDefault();

            return PartialView("PagePartials/TopLinksPartial", new TopLinksPartialViewModel
            {
                IsAuthenticated = user.IsAuthenticated,
                UserName = user.Name,
                ReturnUrl = Request.RawUrl,
                Language = ContentLanguage.PreferredCulture.Name,
                LoginPageLink = loginPage == null ? null : loginPage.PageLink
            });
        }

        public ActionResult ShoppingCartPartial()
        {
            var cart = (Cart)_siteCartService.GetCart(_siteCartService.DefaultCartName);
            return PartialView("PagePartials/ShoppingCartPartial", new ShoppingCartPartialViewModel
            {
                CartName = cart.Name,
                TotalCount = (int)cart.OrderForms.Sum(of => of.LineItems.Sum(li => li.Quantity))
            });
        }
    }
}