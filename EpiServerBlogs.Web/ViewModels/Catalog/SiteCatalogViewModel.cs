using EpiServerBlogs.Web.Models.Catalog;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCatalogViewModel
    {
        public SiteCatalogViewModel(SiteCatalogContent catalogContent, SiteCommerceViewModel[] subCatalogs,
            SiteCommerceViewModel[] products)
        {
            CatalogContent = catalogContent;
            SubCatalogs = subCatalogs ?? new SiteCommerceViewModel[] {};
            Products = products ?? new SiteCommerceViewModel[] {};
        }


        public SiteCatalogContent CatalogContent { get; private set; }

        public SiteCommerceViewModel[] SubCatalogs { get; set; }

        public SiteCommerceViewModel[] Products { get; set; }
    }
}