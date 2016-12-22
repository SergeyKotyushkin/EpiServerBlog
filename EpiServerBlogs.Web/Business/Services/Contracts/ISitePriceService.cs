using EpiServerBlogs.Web.Models.Catalog;
using Mediachase.Commerce;

namespace EpiServerBlogs.Web.Business.Services.Contracts
{
    public interface ISitePriceService
    {
        Money? GetPrice(SiteVariationContent variationContent);

        string GetDisplayPrice(SiteVariationContent variationContent);

        string GetDisplayPrice(Money? price);
    }
}