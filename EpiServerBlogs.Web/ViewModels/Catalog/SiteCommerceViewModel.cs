namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCommerceViewModel
    {
        public SiteCommerceViewModel(SiteCatalogViewModel catalog)
        {
            Catalog = catalog;
            Title = catalog.Name;
        }


        public SiteCatalogViewModel Catalog { get; set; }

        public string Title { get; private set; }
    }
}