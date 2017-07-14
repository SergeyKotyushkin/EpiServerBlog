using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using Mediachase.BusinessFoundation.Data;
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

            if (!User.Identity.IsAuthenticated)
                return Redirect(Url.ContentUrl(ContentReference.StartPage));

            var addresses = _customerContextFacade.CurrentContact.ContactAddresses.ToArray();

            var emailClaim = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var model = new AccountPageViewModel(currentPage)
            {
                UserName = User.Identity.Name,
                Email = emailClaim == null ? string.Empty : emailClaim.Value,
                BillingAddresses =
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Billing)
                        .Select(a => new AddressViewModel(_customerContextFacade, _countryManagerFacade, a)),
                ShippingAddresses =
                    addresses.Where(a => a.AddressType == CustomerAddressTypeEnum.Shipping)
                        .Select(a => new AddressViewModel(_customerContextFacade, _countryManagerFacade, a))
            };
            return View(model);
        }

        public ActionResult Primary(string addressId)
        {
            Guid guid;
            if (addressId == null || !Guid.TryParse(addressId, out guid))
                return RedirectToAction("Index");

            var address =
                _customerContextFacade.CurrentContact.ContactAddresses.FirstOrDefault(
                    a => a.AddressId.Equals(new PrimaryKeyId(guid)));

            if(address == null)
                return RedirectToAction("Index");

            switch (address.AddressType)
            {
                case CustomerAddressTypeEnum.Billing:
                    _customerContextFacade.CurrentContact.PreferredBillingAddress = address;
                    _customerContextFacade.CurrentContact.SaveChanges();
                    break;
                case CustomerAddressTypeEnum.Shipping:
                    _customerContextFacade.CurrentContact.PreferredShippingAddress = address;
                    _customerContextFacade.CurrentContact.SaveChanges();
                    break;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string addressId)
        {
            Guid guid;
            if (addressId == null || !Guid.TryParse(addressId, out guid))
                return RedirectToAction("Index");

            var contact = _customerContextFacade.CurrentContact;
            var address =
                contact.ContactAddresses.FirstOrDefault(
                    a => a.AddressId.Equals(new PrimaryKeyId(guid)));

            if (address == null)
                return RedirectToAction("Index");
            
            if (contact.PreferredBillingAddressId == address.PrimaryKeyId ||
                contact.PreferredShippingAddressId == address.PrimaryKeyId)
            {
                contact.PreferredBillingAddressId = contact.PreferredBillingAddressId == address.PrimaryKeyId
                    ? null
                    : contact.PreferredBillingAddressId;
                contact.PreferredShippingAddressId = contact.PreferredShippingAddressId == address.PrimaryKeyId
                    ? null
                    : contact.PreferredShippingAddressId;
                contact.SaveChanges();
            }

            contact.DeleteContactAddress(address);
            contact.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}