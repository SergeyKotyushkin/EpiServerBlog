using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.ViewModels.Catalog;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers.Catalog
{
    public class SiteCatalogController : ContentController<SiteCatalogContent>
    {
        private readonly IContentLoader _contentLoader;

        public SiteCatalogController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public ActionResult Index(SiteCatalogContent currentContent)
        {
            var subCatalogs = GetCatalogChildren(currentContent);
            var products = GetProductChildren(currentContent);

            var catalogModel = new SiteCatalogViewModel(currentContent, subCatalogs, products);
            var model = new SiteCommerceViewModel(catalogModel);
            return View(model);
        }

        private IEnumerable<SiteSubCatalogViewModel> GetCatalogChildren(IContent catalog)
        {
            var subCatalogs = _contentLoader.GetChildren<SiteCatalogContent>(catalog.ContentLink);

            return subCatalogs.Select(s => new SiteSubCatalogViewModel(s));
        }

        private IEnumerable<SiteCatalogProductViewModel> GetProductChildren(IContent catalog)
        {
            var products = _contentLoader.GetChildren<SiteProductContent>(catalog.ContentLink);

            return products.Select(p => new SiteCatalogProductViewModel(p));
        }
    }
}