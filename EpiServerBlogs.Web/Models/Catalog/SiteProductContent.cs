using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Models.Catalog
{
    [CatalogContentType(GUID = "5e4c09ee-9abd-4f75-8571-9527afb1c178", MetaClassName = "Site_Product_Content")]
    public class SiteProductContent : ProductContent
    {
        [Required]
        [CultureSpecific]
        [Display(
            Name = "Product Title",
            Description = "Page title of the current commerce product",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string ProductTitle { get; set; }

        [Required]
        [CultureSpecific]
        [Display(
            Name = "Product Name",
            Description = "Name of the current commerce product",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string ProductName { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Product Description",
            Description = "Description of the current commerce product",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual XhtmlString ProductDescription { get; set; }

        [Display(
            Name = "Product Image",
            Description = "Image of the current commerce product",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        [UIHint(UIHint.MediaFile)]
        public virtual ContentReference ProductImage { get; set; }
    }
}