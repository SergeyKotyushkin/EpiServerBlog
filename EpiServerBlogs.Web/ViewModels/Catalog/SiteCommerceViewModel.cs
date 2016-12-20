using EpiServerBlogs.Web.Enums;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCommerceViewModel
    {
        public SiteCommerceViewModel(SiteCatalogViewModel catalog)
        {
            Catalog = catalog;
            Title = catalog.CatalogContent.CatalogTitle;
            ChildType = CatalogChildrenTypes.Catalog;
        }

        public SiteCatalogViewModel Catalog { get; set; }

        public string Title { get; private set; }

        public CatalogChildrenTypes ChildType { get; private set; }
    }
}