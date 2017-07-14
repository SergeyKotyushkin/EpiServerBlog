using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Core;
using EPiServer.Web;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCatalogViewModel
    {
        public SiteCatalogViewModel(SiteCatalogContent catalogContent, IEnumerable<SiteSubCatalogViewModel> subCatalogs,
            IEnumerable<SiteCatalogProductViewModel> products)
        {
            Name = catalogContent.CatalogName;
            Description = catalogContent.CatalogDescription;
            Url = catalogContent.ContentLink;
            ImageUrl = catalogContent.CatalogImage;
            SubCatalogs = subCatalogs ?? Enumerable.Empty<SiteSubCatalogViewModel>();
            Products = products ?? Enumerable.Empty<SiteCatalogProductViewModel>();
        }


        public string Name { get; private set; }

        public XhtmlString Description { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageUrl { get; private set; }

        public IEnumerable<SiteSubCatalogViewModel> SubCatalogs { get; set; }

        public IEnumerable<SiteCatalogProductViewModel> Products { get; set; }
    }
}