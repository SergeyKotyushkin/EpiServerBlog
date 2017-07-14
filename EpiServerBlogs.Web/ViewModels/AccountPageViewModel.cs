using System.Collections.Generic;
using System.Linq;
using EpiServerBlogs.Web.Models.Pages;
using EPiServer;
using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels
{
    public class AccountPageViewModel : SitePageViewModel<AccountPage>
    {
        public AccountPageViewModel(AccountPage currentPage) : base(currentPage)
        {
            var addressPage =
                DataFactory.Instance.GetChildren<AddressPage>(ContentReference.StartPage).FirstOrDefault();

            AddressPageLink = addressPage == null ? PageReference.EmptyReference : addressPage.PageLink;
        }


        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<AddressViewModel> BillingAddresses { get; set; }

        public IEnumerable<AddressViewModel> ShippingAddresses { get; set; }

        public PageReference AddressPageLink { get; set; }
    }
}