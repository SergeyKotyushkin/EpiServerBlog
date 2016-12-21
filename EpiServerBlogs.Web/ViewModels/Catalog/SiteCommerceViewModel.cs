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

        public SiteCatalogViewModel Catalog { get; set; }

        public SiteProductViewModel Product { get; set; }

        public string Title { get; private set; }
    }
}