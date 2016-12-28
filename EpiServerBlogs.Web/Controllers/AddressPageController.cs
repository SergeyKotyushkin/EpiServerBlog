using System;
using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using Mediachase.BusinessFoundation.Data;
using Mediachase.Commerce.Customers;

namespace EpiServerBlogs.Web.Controllers
{
    public class AddressPageController : PageControllerBase<AddressPage>
    {
        private readonly CustomerContextFacade _customerContextFacade;
        private readonly CountryManagerFacade _countryManagerFacade;

        public AddressPageController(ISiteCartService siteCartService, CustomerContextFacade customerContextFacade, CountryManagerFacade countryManagerFacade) : base(siteCartService)
        {
            _customerContextFacade = customerContextFacade;
            _countryManagerFacade = countryManagerFacade;
        }
        
        [HttpGet]
        public ActionResult Index(AddressPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var addressId = PrimaryKeyId.Empty;
            var queryAddressId = Request.QueryString["addressId"];
            if (queryAddressId != null)
            {
                Guid guid;
                if (Guid.TryParse(queryAddressId, out guid))
                    addressId = new PrimaryKeyId(guid);
            }
            
            var address = _customerContextFacade.CurrentContact.ContactAddresses
                .FirstOrDefault(a => a.AddressId.Equals(addressId));
            var model = new AddressPageViewModel(currentPage)
            {
                AddressViewModel = new AddressViewModel(_customerContextFacade, _countryManagerFacade, address)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AddressPage currentPage, AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var indexModel = new AddressPageViewModel(currentPage)
                {
                    AddressViewModel = model
                };
                return View(indexModel);
            }

            bool isExists;
            var address = GetAddress(model, out isExists);

            if (isExists)
                _customerContextFacade.CurrentContact.UpdateContactAddress(address);
            else
                _customerContextFacade.CurrentContact.AddContactAddress(address);

            _customerContextFacade.CurrentContact.SaveChanges();

            var accountPage = DataFactory.Instance.GetChildren<AccountPage>(ContentReference.StartPage).FirstOrDefault();
            var pageLink = accountPage == null ? ContentReference.StartPage : accountPage.PageLink;

            return Redirect(Url.ContentUrl(pageLink));
        }

        private CustomerAddress GetAddress(AddressViewModel model, out bool isExists)
        {
            CustomerAddress address = null;
            Guid guid;
            if (model.AddressId != null && Guid.TryParse(model.AddressId, out guid))
            {
                var addressId = new PrimaryKeyId(guid);

                address =
                    _customerContextFacade.CurrentContact.ContactAddresses.FirstOrDefault(
                        a => a.AddressId.Equals(addressId));
            }

            isExists = true;
            if (address == null)
            {
                address = CustomerAddress.CreateInstance();
                isExists = false;
            }

            address.Name = model.Name;
            address.City = model.City;
            address.CountryCode = model.CountryCode;
            address.CountryName =
                _countryManagerFacade.GetCountries()
                    .Country.Where(x => x.Code == model.CountryCode)
                    .Select(x => x.Name)
                    .FirstOrDefault();
            address.FirstName = model.FirstName;
            address.LastName = model.LastName;
            address.Line1 = model.Line1;
            address.Line2 = model.Line2;
            address.DaytimePhoneNumber = model.DaytimePhoneNumber;
            address.PostalCode = model.PostalCode;
            address.RegionName = model.RegionName;
            address.RegionCode = model.RegionCode;
            // Commerce Manager expects State to be set for addresses in order management. Set it to be same as
            // RegionName to avoid issues.
            address.State = model.RegionName;
            address.Email = model.Email;
            address.AddressType = (CustomerAddressTypeEnum) (model.AddressType ?? (int) CustomerAddressTypeEnum.Billing);

            return address;
        }
    }
}