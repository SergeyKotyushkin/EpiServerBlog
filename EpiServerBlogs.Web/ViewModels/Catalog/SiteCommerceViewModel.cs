namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCommerceViewModel
    {
        public SiteCommerceViewModel(SiteCatalogViewModel catalog)
        {
            Catalog = catalog;
            Title = catalog.Name;
        }

        public SiteCommerceViewModel(SiteProductViewModel product)
        {
            Product = product;
            Title = product.Name;
        }

        public SiteCommerceViewModel(SiteVariationViewModel variation)
        {
            Variation = variation;
            Title = variation.Name;
        }

        public SiteCatalogViewModel Catalog { get; set; }

        public SiteProductViewModel Product { get; set; }

        public SiteVariationViewModel Variation { get; set; }

        public string Title { get; private set; }
    }
}