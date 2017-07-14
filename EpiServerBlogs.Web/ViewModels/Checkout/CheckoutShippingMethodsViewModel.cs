using System.Collections.Generic;
using Mediachase.Commerce.Orders;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutShippingMethodsViewModel
    {
        public IEnumerable<ShippingRate> ShippingRates { get; set; }  
    }
}