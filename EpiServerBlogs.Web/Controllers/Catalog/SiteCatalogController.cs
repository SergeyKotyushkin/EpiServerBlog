using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteCatalogController : ContentController<SiteCatalogContent>
    {
        public ActionResult Index(SiteCatalogContent currentContent)
        {
            var children = GetCatalogChildren(currentContent);

            var model = new SiteCommerceViewModel(new SiteCatalogViewModel(currentContent, children));
            return View(model);
        }

        private static SiteCommerceViewModel[] GetCatalogChildren(IContent catalog)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var children = contentLoader.GetChildren<SiteCatalogContent>(catalog.ContentLink);

            var resultList = new List<SiteCommerceViewModel>();

            resultList.AddRange(children.Select(c => new SiteCommerceViewModel(new SiteCatalogViewModel(c))));

            return resultList.ToArray();
        }
    }
}