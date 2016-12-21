using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Models.Catalog
{
    [CatalogContentType(GUID = "3d36ce19-bf86-475b-9f0a-a30c79aa9faa", MetaClassName = "Site_Variation_Content")]
    public class SiteVariationContent : VariationContent
    {
        [Required]
        [CultureSpecific]
        [Display(
            Name = "Variation Title",
            Description = "Page title of the current commerce variation",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string VariationTitle { get; set; }

        [Required]
        [CultureSpecific]
        [Display(
            Name = "Variation Name",
            Description = "Name of the current commerce variation",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string VariationName { get; set; }
        
        [Required]
        [CultureSpecific]
        [Display(
            Name = "Variation Description",
            Description = "Description of the current commerce variation",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual XhtmlString VariationDescription { get; set; }

        [Display(
            Name = "Variation Image",
            Description = "Image of the current commerce variation",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        [UIHint(UIHint.MediaFile)]
        public virtual ContentReference VariationImage { get; set; }
    }
}