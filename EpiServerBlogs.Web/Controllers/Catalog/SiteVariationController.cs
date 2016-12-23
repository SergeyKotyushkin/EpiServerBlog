using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;
using EPiServer;
using EPiServer.Commerce.Order;
using Mediachase.Commerce.Catalog;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteVariationController : ContentControllerBase<SiteVariationContent>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISiteCartService _siteCartService;
        private readonly ReferenceConverter _referenceConverter;

        public SiteVariationController(IContentRepository contentRepository, IOrderRepository orderRepository,
            ISiteCartService siteCartService, ReferenceConverter referenceConverter) : base(siteCartService)
        {
            _contentRepository = contentRepository;
            _referenceConverter = referenceConverter;
            _orderRepository = orderRepository;
            _siteCartService = siteCartService;
        }

        public ActionResult Index(SiteVariationContent currentContent)
        {
            var variationContent = new SiteVariationViewModel(currentContent);
            var model = new SiteCommerceViewModel(variationContent);
            return View(model);
        }

        public ActionResult AddToCart(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Content("Variation code is empty");
            }

            var variationContentLink = _referenceConverter.GetContentLink(code);
            var variationContent = _contentRepository.Get<SiteVariationContent>(variationContentLink);

            if (variationContent == null)
            {
                return Content("Variation code does not exist");
            }

            var cart = _siteCartService.GetCart(_siteCartService.DefaultCartName);

            string errorMessage;
            if (_siteCartService.AddToCart(cart, variationContent, out errorMessage))
            {
                _orderRepository.Save(cart);
            }
            else
            {
                return Content("Not added");
            }


            return Content("Added");
        }
    }
}