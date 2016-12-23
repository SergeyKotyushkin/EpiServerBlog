using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteProductController : ContentControllerBase<SiteProductContent>
    {
        public SiteProductController(ISiteCartService siteCartService) : base(siteCartService)
        {
        }

        public ActionResult Index(SiteProductContent currentContent)
        {
            var productModel = new SiteProductViewModel(currentContent);
            var model = new SiteCommerceViewModel(productModel);
            return View(model);
        }
    }
}