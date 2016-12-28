using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Controllers
{
    public class CheckoutPageController : PageControllerBase<CheckoutPage>
    {
        private readonly ISiteCartService _siteCartService;
        private readonly IContentRepository _contentRepository;
        private readonly ReferenceConverter _referenceConverter;
        private readonly CustomerContextFacade _customerContextFacade;

        public CheckoutPageController(ReferenceConverter referenceConverter, CustomerContextFacade customerContextFacade,
            ISiteCartService siteCartService, IContentRepository contentRepository) : base(siteCartService)
        {
            _referenceConverter = referenceConverter;
            _customerContextFacade = customerContextFacade;
            _siteCartService = siteCartService;
            _contentRepository = contentRepository;
        }

        public ActionResult Index(CheckoutPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var cart = (Cart) _siteCartService.GetCart(_siteCartService.DefaultCartName);
            if (_siteCartService.IsEmptyCart(cart))
                return Redirect(Url.ContentUrl(ContentReference.StartPage));

            var model = new CheckoutPageViewModel(currentPage, cart, _contentRepository, _referenceConverter,
                _customerContextFacade);
            return View(model);
        }

        [HttpPost]
        public ActionResult PlaceOrder()
        {
            var cart = _siteCartService.GetCart(_siteCartService.DefaultCartName);
            if (_siteCartService.IsEmptyCart(cart))
                return Redirect(Url.ContentUrl(ContentReference.StartPage));
            
            return RedirectToAction("Index");
        }
    }
}