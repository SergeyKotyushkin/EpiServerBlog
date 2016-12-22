using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Commerce.Order;

namespace EpiServerBlogs.Web.Business.Services.Contracts
{
    public interface ISiteCartService
    {
        ICart GetCart(string name);

        bool AddToCart(ICart cart, SiteVariationContent variationContent, out string errorMessage);

        bool RemoveFromCart(ICart cart, string code, out string errorMessage);

        string DefaultCartName { get; }
    }
}