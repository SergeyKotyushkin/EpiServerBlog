using System.Linq;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;

namespace EpiServerBlogs.Web.Business.Services
{
    public class SitePriceService : ISitePriceService
    {
        public Money? GetPrice(SiteVariationContent variationContent)
        {
            var price = variationContent.GetPrices().FirstOrDefault();
            return price == null ? null : (Money?) price.UnitPrice;
        }

        public string GetDisplayPrice(SiteVariationContent variationContent)
        {
            var price = GetPrice(variationContent);
            return GetDisplayPrice(price);
        }

        public string GetDisplayPrice(Money? price)
        {
            return price == null ? null : price.ToString();
        }
    }
}