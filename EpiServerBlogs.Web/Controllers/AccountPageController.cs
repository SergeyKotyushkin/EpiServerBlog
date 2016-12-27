using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using Mediachase.Commerce.Customers;

namespace EpiServerBlogs.Web.Controllers
{
    public class AccountPageController : PageControllerBase<AccountPage>
    {
        private readonly CustomerContextFacade _customerContextFacade;
        private readonly CountryManagerFacade _countryManagerFacade;

        public AccountPageController(ISiteCartService siteCartService, CustomerContextFacade customerContextFacade,
            CountryManagerFacade countryManagerFacade)
            : base(siteCartService)
        {
            _customerContextFacade = customerContextFacade;
            _countryManagerFacade = countryManagerFacade;
        }

        public ActionResult Index(AccountPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var addresses = _customerContextFacade.CurrentContact.ContactAddresses.ToArray();

            var model = new AccountPageViewModel(currentPage)
            {
                UserName = User.Identity.Name,
                Email = _customerContextFacade.CurrentContact.ContactEmail,
                BillingAddresses =
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Billing)
                        .Select(a => new AddressViewModel(_customerContextFacade, _countryManagerFacade, a)),
                ShippingAddresses = 
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Shipping)
                        .Select(a => new AddressViewModel(_customerContextFacade, _countryManagerFacade, a))
            };
            return View(model);
        }
    }
}