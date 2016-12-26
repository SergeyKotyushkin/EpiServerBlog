using EPiServer.Core;
using Mediachase.Commerce;
using Mediachase.Commerce.Core;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutLineItemViewModel
    {
        public string VariationDisplayName { get; set; }

        public string VariationCode { get; set; }

        public ContentReference VariationLink { get; set; }

        public ContentReference VariationImageLink { get; set; }

        public decimal PlacedPrice { get; set; }

        public decimal ExtendedPrice { get; set; }

        public int Quantity { get; set; }

        public string DisplayPlacedPrice
        {
            get { return new Money(PlacedPrice, SiteContext.Current.Currency).ToString(); }
        }

        public string DisplayExtendedPrice
        {
            get { return new Money(ExtendedPrice, SiteContext.Current.Currency).ToString(); }
        }
    }
}