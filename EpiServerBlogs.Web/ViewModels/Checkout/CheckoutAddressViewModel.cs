using Mediachase.BusinessFoundation.Data;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutAddressViewModel
    {
        public PrimaryKeyId AddressId { get; set; }

        public string Name { get; set; }
        
        public string ContactInfo { get; set; }
        
        public string AddressInfo { get; set; }

        public bool IsPrimary { get; set; }
    }
}