using System.ComponentModel.DataAnnotations;
using System.Linq;
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
#pragma warning restore 649

        public SiteVariationViewModel(SiteVariationContent variationContent)
        {
            Name = variationContent.VariationName;
            Description = variationContent.VariationDescription;
            Url = variationContent.ContentLink;
            ImageLink = variationContent.VariationImage;

            var inventory = _inventoryService.Service.QueryByEntry(new[] {variationContent.Code}).FirstOrDefault();
            InventoryQuantity = inventory == null ? 0 : (int) inventory.PurchaseAvailableQuantity;

            var price = variationContent.GetPrices().FirstOrDefault();
            Price = price == null ? null : price.UnitPrice.ToString();
        }


        public string Name { get; private set; }

        public XhtmlString Description { get; private set; }

        public ContentReference Url { get; private set; }

        [UIHint(UIHint.MediaFile)]
        public ContentReference ImageLink { get; private set; }

        public int InventoryQuantity { get; private set; }

        public string Price { get; private set; }
    }
}