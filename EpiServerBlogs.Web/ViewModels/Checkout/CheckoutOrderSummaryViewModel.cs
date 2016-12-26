using EpiServerBlogs.Web.Business.Extensions;
using Mediachase.Commerce.Core;

namespace EpiServerBlogs.Web.ViewModels.Checkout
{
    public class CheckoutOrderSummaryViewModel
    {
        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }


        public string DisplaySubTotal
        {
            get { return SubTotal.ToMoney(SiteContext.Current.Currency); }
        }

        public string DisplayDiscount
        {
            get { return Discount.ToMoney(SiteContext.Current.Currency); }
        }

        public string DisplayShipping
        {
            get { return Shipping.ToMoney(SiteContext.Current.Currency); }
        }

        public string DisplayTotal
        {
            get { return Total.ToMoney(SiteContext.Current.Currency); }
        }
    }
}