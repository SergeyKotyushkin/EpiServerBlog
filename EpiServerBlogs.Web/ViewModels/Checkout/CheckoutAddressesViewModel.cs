using System.Collections.Generic;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutAddressesViewModel
    {
        public IEnumerable<CheckoutAddressViewModel> BillingAddresses { get; set; } 
        
        public IEnumerable<CheckoutAddressViewModel> ShippingAddresses { get; set; } 

        public CheckoutAddressViewModel SelectedBillingAddress { get; set; } 

        public CheckoutAddressViewModel SelectedShippingAddress { get; set; } 
    }
}