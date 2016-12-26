using System;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Pricing;

namespace EpiServerBlogs.Web.Business.Services
{
    public class SitePriceService : ISitePriceService
    {
        private readonly IPriceService _priceService;
        private readonly ICurrentMarket _currentMarket;
        private readonly AppContextFacade _appContextFacade;

        public SitePriceService(IPriceService priceService, ICurrentMarket currentMarket,
            AppContextFacade appContextFacade)
        {
            _priceService = priceService;
            _currentMarket = currentMarket;
            _appContextFacade = appContextFacade;
        }

        public Money? GetPrice(SiteVariationContent variationContent)
        {
            var price = _priceService.GetDefaultPrice(
                _currentMarket.GetCurrentMarket().MarketId,
                DateTime.Now,
                new CatalogKey(_appContextFacade.ApplicationId, variationContent.Code),
                SiteContext.Current.Currency);
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