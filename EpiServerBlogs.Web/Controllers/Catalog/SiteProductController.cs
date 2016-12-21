using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteProductController : ContentController<SiteProductContent>
    {
        public ActionResult Index(SiteProductContent currentContent)
        {
            // TODO: IMPLEMENT
            return Content("HAS NOT BEEN IMPLEMENTED YET");
        }
    }
}