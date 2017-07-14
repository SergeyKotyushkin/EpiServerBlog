using System.Linq;
using EpiServerBlogs.Web.Models.Pages;
using EPiServer;
using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels
{
    public class AddressPageViewModel : SitePageViewModel<AddressPage>
    {
        public AddressPageViewModel(AddressPage currentPage) : base(currentPage)
        {
            var accountPage =
                DataFactory.Instance.GetChildren<AccountPage>(ContentReference.StartPage).FirstOrDefault();

            AccountPageLink = accountPage == null ? PageReference.EmptyReference : accountPage.PageLink;
        }

        public AddressViewModel AddressViewModel { get; set; }

        public PageReference AccountPageLink { get; set; }
    }
}