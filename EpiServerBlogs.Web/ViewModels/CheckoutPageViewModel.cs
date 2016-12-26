using System.Linq;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Checkout;
using EPiServer;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.ViewModels
{
    public class CheckoutPageViewModel : SitePageViewModel<CheckoutPage>
    {
        public Cart Cart { get; set; }

        public CheckoutCartItemsViewModel CartItemsViewModel { get; set; }
 
        public CheckoutPageViewModel(CheckoutPage currentPage, Cart cart) : base(currentPage)
        {
            Cart = cart;

            var lineItems = cart.OrderForms.Any()
                ? cart.OrderForms[0].LineItems
                : Enumerable.Empty<LineItem>();

            var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            CartItemsViewModel = new CheckoutCartItemsViewModel
            {
                ListItems = lineItems.Select(li =>
                {
                    var variationContentLink = referenceConverter.GetContentLink(li.Code);
                    var variationContent = contentRepository.Get<SiteVariationContent>(variationContentLink);
                    return new CheckoutLineItemViewModel
                    {
                        VariationDisplayName = li.DisplayName,
                        VariationCode = li.Code,
                        VariationLink = variationContentLink,
                        VariationImageLink = variationContent.VariationImage,
                        PlacedPrice = li.PlacedPrice,
                        ExtendedPrice = li.ExtendedPrice,
                        Quantity = (int) li.Quantity
                    };
                })
            };
        }
    }
}