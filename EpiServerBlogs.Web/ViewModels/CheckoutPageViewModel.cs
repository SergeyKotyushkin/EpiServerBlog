using System;
using System.Collections.Generic;
using System.Linq;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Models.Catalog;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Checkout;
using EPiServer;
using EPiServer.Globalization;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;

namespace EpiServerBlogs.Web.ViewModels
{
    public class CheckoutPageViewModel : SitePageViewModel<CheckoutPage>
    {

        public Cart Cart { get; set; }

        public CheckoutCartItemsViewModel CartItemsViewModel { get; set; }

        public CheckoutShippingMethodsViewModel ShippingMethodsViewModel { get; set; }

        public CheckoutOrderSummaryViewModel OrderSummaryViewModel { get; set; }

        public CheckoutAddressesViewModel AddressesViewModel { get; set; }

        public CheckoutPageViewModel(CheckoutPage currentPage, Cart cart, 
            IContentLoader contentRepository,
            ReferenceConverter referenceConverter,
            CustomerContextFacade customerContextFacade) : base(currentPage)
        {
            Cart = cart;

            var lineItems = cart.OrderForms.Any()
                ? cart.OrderForms[0].LineItems
                : Enumerable.Empty<LineItem>();

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

            ShippingMethodsViewModel = new CheckoutShippingMethodsViewModel {ShippingRates = GetShippingMethods(cart)};

            AddressesViewModel = GetAddressesViewModel(customerContextFacade);

            OrderSummaryViewModel = new CheckoutOrderSummaryViewModel
            {
                SubTotal = cart.SubTotal,
                Discount = cart.TaxTotal,
                Shipping = cart.ShippingTotal,
                Total = cart.Total
            };
        }

        private static IEnumerable<ShippingRate> GetShippingMethods(OrderGroup cart)
        {
            var methods = ShippingManager.GetShippingMethods(SiteContext.Current.LanguageName);

            // filter the list for only methods that apply to this particular cart's shipping address 
            var shippingRows = methods.ShippingMethod.Rows.Cast<ShippingMethodDto.ShippingMethodRow>().ToList();

            var list = new List<ShippingRate>();
            foreach (var row in shippingRows)
            {
                var type = Type.GetType(row.ShippingOptionRow.ClassName);
                if (type == null)
                    continue;

                var message = string.Empty;
                var provider = (IShippingGateway) Activator.CreateInstance(type);

                var shipments = cart.OrderForms[0].Shipments.ToArray();

                if (shipments.Length == 0)
                    continue;

                list.AddRange(shipments.Select(shipment => provider.GetRate(row.ShippingMethodId, shipment, ref message)));
            }
            return list.Where(s => s != null);
        }

        private static CheckoutAddressesViewModel GetAddressesViewModel(CustomerContextFacade customerContextFacade)
        {
            var preferedBillingAddress = customerContextFacade.CurrentContact.PreferredBillingAddress;
            var preferedShippingAddress = customerContextFacade.CurrentContact.PreferredShippingAddress;
            var addresses = customerContextFacade.CurrentContact.ContactAddresses.ToArray();
            var model = new CheckoutAddressesViewModel
            {
                BillingAddresses =
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Billing)
                        .Select(a => new CheckoutAddressViewModel
                        {
                            AddressId = a.AddressId,
                            Name = a.Name,
                            ContactInfo = string.Format("{0} {1}", a.FirstName, a.LastName),
                            AddressInfo =
                                string.Format("{0} {1} {2} {3} {4}", a.CountryName, a.State, a.City, a.Line1,
                                    a.PostalCode),
                            IsPrimary =
                                preferedBillingAddress != null && preferedBillingAddress.AddressId.Equals(a.AddressId)
                        }),
                ShippingAddresses =
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Shipping)
                        .Select(a => new CheckoutAddressViewModel
                        {
                            AddressId = a.AddressId,
                            Name = a.Name,
                            ContactInfo = string.Format("{0} {1}", a.FirstName, a.LastName),
                            AddressInfo =
                                string.Format("{0} {1} {2} {3} {4}", a.CountryName, a.State, a.City, a.Line1,
                                    a.PostalCode),
                            IsPrimary =
                                preferedShippingAddress != null && preferedShippingAddress.AddressId.Equals(a.AddressId)
                        })
            };
            model.SelectedBillingAddress =
                model.BillingAddresses.FirstOrDefault(a => a.IsPrimary);
            model.SelectedShippingAddress =
                model.ShippingAddresses.FirstOrDefault(a => a.IsPrimary);

            return model;
        }
    }
}