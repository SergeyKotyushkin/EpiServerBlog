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
            var subCatalogs = GetCatalogChildren(currentContent);
            var products = GetProductChildren(currentContent);

            var model = new SiteCommerceViewModel(new SiteCatalogViewModel(currentContent, subCatalogs, products));
            return View(model);
        }

        private static SiteCommerceViewModel[] GetCatalogChildren(IContent catalog)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var subCatalogs = contentLoader.GetChildren<SiteCatalogContent>(catalog.ContentLink);

            return
                subCatalogs.Select(
                    s =>
                        new SiteCommerceViewModel(new SiteCatalogViewModel(s, null, null))).ToArray();
        }

        private static SiteCommerceViewModel[] GetProductChildren(IContent catalog)
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var products = contentLoader.GetChildren<SiteProductContent>(catalog.ContentLink);

            return products.Select(p => new SiteCommerceViewModel(new SiteProductViewModel(p))).ToArray();
        }
    }
}