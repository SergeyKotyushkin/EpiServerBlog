using EpiServerBlogs.Web.Models.Pages;
namespace EpiServerBlogs.Web.ViewModels
{
    public class AddressPageViewModel : SitePageViewModel<AddressPage>
    {
        public AddressPageViewModel(AddressPage currentPage) : base(currentPage)
        {
        }

        public AddressViewModel AddressViewModel { get; set; }
    }
}