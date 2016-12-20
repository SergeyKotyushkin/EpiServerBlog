using EpiServerBlogs.Web.Models.Catalog;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteProductViewModel
    {
        public SiteProductViewModel(SiteProductContent productContent, SiteVariationViewModel[] variations = null)
        {
            ProductContent = productContent;
            Variations = variations ?? new SiteVariationViewModel[] { };
        }


        public SiteProductContent ProductContent { get; private set; }

        public SiteVariationViewModel[] Variations { get; set; }
    }
}