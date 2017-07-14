using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EpiServerBlogs.Web.Business.Extensions;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Core;
using EPiServer.Web;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteProductViewModel
    {
        public SiteProductViewModel(SiteProductContent productContent)
        {
            Name = productContent.ProductName;
            Description = productContent.ProductDescription;
            Url = productContent.ContentLink;
            ImageLink = productContent.ProductImage;
            Variations = productContent.GetVariations().Select(v => new SiteProductVariationViewModel(v));
        }


        public string Name { get; private set; }

        public XhtmlString Description { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageLink { get; private set; }


        public IEnumerable<SiteProductVariationViewModel> Variations { get; private set; }
    }
}