using EpiServerBlogs.Web.Models.Catalog;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteCatalogViewModel
    {
        public SiteCatalogViewModel(SiteCatalogContent catalogContent, SiteCommerceViewModel[] children = null)
        {
            CatalogContent = catalogContent;
            Children = children ?? new SiteCommerceViewModel[] {};
        }


        public SiteCatalogContent CatalogContent { get; private set; }

        public SiteCommerceViewModel[] Children { get; set; }
    }
}