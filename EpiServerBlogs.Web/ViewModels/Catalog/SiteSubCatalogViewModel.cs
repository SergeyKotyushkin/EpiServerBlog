using System.ComponentModel.DataAnnotations;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Core;
using EPiServer.Web;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteSubCatalogViewModel
    {
        public SiteSubCatalogViewModel(SiteCatalogContent catalogContent)
        {
            Name = catalogContent.CatalogName;
            Url = catalogContent.ContentLink;
            ImageUrl = catalogContent.CatalogImage;
        }


        public string Name { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageUrl { get; private set; }
    }
}