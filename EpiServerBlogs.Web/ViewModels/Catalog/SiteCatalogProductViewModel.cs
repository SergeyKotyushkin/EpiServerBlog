using System.ComponentModel.DataAnnotations;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Core;
using EPiServer.Web;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCatalogProductViewModel
    {
        public SiteCatalogProductViewModel(SiteProductContent productContent)
        {
            Name = productContent.ProductName;
            Url = productContent.ContentLink;
            ImageUrl = productContent.ProductImage;
        }


        public string Name { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageUrl { get; private set; }
    }
}