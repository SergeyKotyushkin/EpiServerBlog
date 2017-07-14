using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels.Partial
{
    public class ShoppingCartPartialViewModel
    {
        public string CartName { get; set; }

        public int TotalCount { get; set; }

        public PageReference CheckoutPageLink { get; set; }

        public string CartTitle
        {
            get { return string.Format("{0} ({1})", CartName, TotalCount); }
        }

        public bool IsEmpty
        {
            get { return TotalCount == 0; }
        }
    }
}