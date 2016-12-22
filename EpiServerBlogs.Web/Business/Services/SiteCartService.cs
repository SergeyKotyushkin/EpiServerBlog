using System.Linq;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer.Commerce.Order;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.Business.Services
{
    public class SiteCartService : ISiteCartService
    {
        private readonly IOrderFactory _orderFactory;
        private readonly ISitePriceService _sitePriceService;
        private readonly CustomerContextFacade _customerContextFacade;

        public SiteCartService(IOrderFactory orderFactory, ISitePriceService sitePriceService,
            CustomerContextFacade customerContextFacade)
        {
            _orderFactory = orderFactory;
            _sitePriceService = sitePriceService;
            _customerContextFacade = customerContextFacade;
        }


        public ICart GetCart(string name)
        {
            var cart = OrderContext.Current.GetCart(name, _customerContextFacade.CurrentContactId);

            return cart;
        }

        public bool AddToCart(ICart cart, SiteVariationContent variationContent, out string errorMessage)
        {
            errorMessage = string.Empty;

            var lineItem = cart.GetAllLineItems().FirstOrDefault(x => x.Code == variationContent.Code);

            var price = _sitePriceService.GetPrice(variationContent);
            if (lineItem == null)
            {
                lineItem = _orderFactory.CreateLineItem(variationContent.Code);
                lineItem.Quantity = 1;
                lineItem.PlacedPrice = price == null ? decimal.Zero : price.Value.Amount;
                cart.AddLineItem(lineItem, _orderFactory);
            }
            else
            {
                var shipment = cart.GetFirstShipment();
                cart.UpdateLineItemQuantity(shipment, lineItem, lineItem.Quantity + 1);
            }

            return true;
        }

        public bool RemoveFromCart(ICart cart, string code, out string errorMessage)
        {
            // TODO: IMPLEMENT
            errorMessage = string.Empty;
            return false;
        }

        public string DefaultCartName
        {
            get { return Cart.DefaultName; }
        }
    }
}