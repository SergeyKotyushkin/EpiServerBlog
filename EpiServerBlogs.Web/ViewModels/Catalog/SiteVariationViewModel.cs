using System.ComponentModel.DataAnnotations;
using System.Linq;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Mediachase.Commerce.InventoryService;

namespace EpiServerBlogs.Web.ViewModels.Catalog
{
    public class SiteVariationViewModel
    {
#pragma warning disable 649
        private static Injected<IInventoryService> _inventoryService;
        private static Injected<IContentLoader> _contentLoader;
        private static Injected<ISitePriceService> _sitePriceService;
#pragma warning restore 649

        public SiteVariationViewModel(SiteVariationContent variationContent)
        {
            Name = variationContent.VariationName;
            Description = variationContent.VariationDescription;
            Url = variationContent.ContentLink;
            ImageLink = variationContent.VariationImage;

            var inventory = _inventoryService.Service.QueryByEntry(new[] {variationContent.Code}).FirstOrDefault();
            InventoryQuantity = inventory == null ? 0 : (int) inventory.PurchaseAvailableQuantity;

            Price = _sitePriceService.Service.GetDisplayPrice(variationContent);

            Code = variationContent.Code;
        }


        public string Name { get; private set; }

        public XhtmlString Description { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageLink { get; private set; }

        public int InventoryQuantity { get; private set; }

        public string Price { get; private set; }

        public string Code { get; private set; }

        public bool ShowPrice
        {
            get { return !string.IsNullOrEmpty(Price); }
        }

        public bool ShowAddToCart
        {
            get { return ShowPrice && InventoryQuantity > decimal.Zero; }
        }
    }
}