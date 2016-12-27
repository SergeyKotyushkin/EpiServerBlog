using System;
using System.Linq;
using System.Web.Mvc;
using AuthorizeNet;
using EpiServerBlogs.Web.Business.Facades;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
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
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            var indexModel = new AddressPageViewModel(currentPage)
            {
                AddressViewModel = model
            };
            return View(indexModel);
        }
    }
}