using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Controllers
{
    public class CheckoutPageController : PageControllerBase<CheckoutPage>
    {
        private readonly ISiteCartService _siteCartService;
        private readonly IContentRepository _contentRepository;
        private readonly ReferenceConverter _referenceConverter;

        public CheckoutPageController(ReferenceConverter referenceConverter, ISiteCartService siteCartService,
            IContentRepository contentRepository) : base(siteCartService)
        {
            _referenceConverter = referenceConverter;
            _siteCartService = siteCartService;
            _contentRepository = contentRepository;
        }

        public ActionResult Index(CheckoutPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var cart = (Cart) _siteCartService.GetCart(_siteCartService.DefaultCartName);
            var model = new CheckoutPageViewModel(currentPage, cart, _contentRepository, _referenceConverter);
            return View(model);
        }
    }
}