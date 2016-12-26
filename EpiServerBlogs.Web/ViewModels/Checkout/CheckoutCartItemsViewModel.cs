using System.Collections.Generic;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutCartItemsViewModel
    {
        public IEnumerable<CheckoutLineItemViewModel> ListItems { get; set; }  
    }
}