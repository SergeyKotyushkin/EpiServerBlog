using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteVariationController : ContentController<SiteVariationContent>
    {
        public ActionResult Index(SiteVariationContent currentContent)
        {
            var variationContent = new SiteVariationViewModel(currentContent);
            var model = new SiteCommerceViewModel(variationContent);
            return View(model);
        }
    }
}