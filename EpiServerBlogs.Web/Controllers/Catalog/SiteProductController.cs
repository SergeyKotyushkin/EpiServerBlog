using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteProductController : ContentController<SiteProductContent>
    {
        public ActionResult Index(SiteProductContent currentContent)
        {
            var productModel = new SiteProductViewModel(currentContent);
            var model = new SiteCommerceViewModel(productModel);
            return View(model);
        }
    }
}