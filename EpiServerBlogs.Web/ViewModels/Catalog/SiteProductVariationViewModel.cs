using System.ComponentModel.DataAnnotations;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Core;
using EPiServer.Web;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteProductVariationViewModel
    {
        public SiteProductVariationViewModel(SiteVariationContent variationContent)
        {
            Name = variationContent.VariationName;
            Description = variationContent.VariationDescription;
            Url = variationContent.ContentLink;
            ImageLink = variationContent.VariationImage;
        }

        public string Name { get; private set; }

        public XhtmlString Description { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageLink { get; private set; } 
    }
}