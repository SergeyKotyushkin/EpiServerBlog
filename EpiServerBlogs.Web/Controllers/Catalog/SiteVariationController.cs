using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteVariationController : ContentController<SiteVariationContent>
    {
        public ActionResult Index(SiteVariationContent currentContent)
        {
            // TODO: IMPLEMENT
            return Content("HAS NOT BEEN MPLEMENTED YET");
        }
    }
}