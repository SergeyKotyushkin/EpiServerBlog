using System.ComponentModel.DataAnnotations;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Models.Catalog
{
    [CatalogContentType(GUID = "ef31c2c1-29ab-401d-9630-5564acec3fc2", MetaClassName = "Site_Catalog_Content")]
    public class SiteCatalogContent : NodeContent
    {
        [Required]
        [CultureSpecific]
        [Display(
            Name = "Catalog Title",
            Description = "Page title of the current commerce catalog",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string CatalogTitle { get; set; }

        [Required]
        [CultureSpecific]
        [Display(
            Name = "Catalog Name",
            Description = "Name of the current commerce catalog",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual string CatalogName { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Catalog Description",
            Description = "Description of the current commerce catalog",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        public virtual XhtmlString CatalogDescription { get; set; }

        [Display(
            Name = "Catalog Image",
            Description = "Image of the current commerce catalog",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        [UIHint(UIHint.MediaFile)]
        public virtual ContentReference CatalogImage { get; set; }
    }
}