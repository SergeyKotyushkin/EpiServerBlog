using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Controllers
{
    public class CheckoutPageController : PageControllerBase<CheckoutPage>
    {
        private readonly ISiteCartService _siteCartService;

        public CheckoutPageController(ISiteCartService siteCartService) : base(siteCartService)
        {
            _siteCartService = siteCartService;
        }

        public ActionResult Index(CheckoutPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var cart = (Cart)_siteCartService.GetCart(_siteCartService.DefaultCartName);
            var model = new CheckoutPageViewModel(currentPage, cart);
            return View(model);
        }
    }
}