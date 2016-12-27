using System.ComponentModel.DataAnnotations;
using System.Linq;
using EpiServerBlogs.Web.Business.Facades;
using Mediachase.BusinessFoundation.Data;
using Mediachase.Commerce.Customers;

namespace EpiServerBlogs.Web.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel(CustomerContextFacade customerContextFacade, CountryManagerFacade countryManagerFacade,
            CustomerAddress address)
        {
            var customerContextFacade1 = customerContextFacade;

            AddressId = address == null ? null : address.AddressId.ToString();
            ContactId = customerContextFacade1.CurrentContactId.ToString();

            if(address == null)
                return;

            if (!address.AddressId.Equals(PrimaryKeyId.Empty))
            {
                AddressType = address.AddressType;

                var contactId = address.ContactId ?? new PrimaryKeyId(customerContextFacade1.CurrentContactId);
                ContactId = contactId.ToString();
            }

            Name = address.Name;
            City = address.City;
            CountryCode = address.CountryCode;
            CountryName =
                countryManagerFacade.GetCountries()
                    .Country.Where(x => x.Code == address.CountryCode)
                    .Select(x => x.Name)
                    .FirstOrDefault();
            FirstName = address.FirstName;
            LastName = address.LastName;
            Line1 = address.Line1;
            Line2 = address.Line2;
            DaytimePhoneNumber = address.DaytimePhoneNumber;
            PostalCode = address.PostalCode;
            RegionName = address.RegionName;
            RegionCode = address.RegionCode;
            // Commerce Manager expects State to be set for addresses in order management. Set it to be same as
            // RegionName to avoid issues.
            State = address.RegionName;
            Email = address.Email;
            AddressType = address.AddressType;
        }

        public AddressViewModel()
        {
        }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City", Prompt = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country Code is required")]
        [Display(Name = "Country Code", Prompt = "Country Code")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Country Name is required")]
        [Display(Name = "Country Name", Prompt = "Country Name")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name", Prompt = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name", Prompt = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Line1 is required")]
        [Display(Name = "Line1", Prompt = "Line1")]
        public string Line1 { get; set; }

        [Required(ErrorMessage = "Line2 is required")]
        [Display(Name = "Line2", Prompt = "Line2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage = "Daytime Phone Number is required")]
        [Display(Name = "Daytime Phone Number", Prompt = "Daytime Phone Number")]
        public string DaytimePhoneNumber { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [Display(Name = "Postal Code", Prompt = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Region Name is required")]
        [Display(Name = "Region Name", Prompt = "Region Name")]
        public string RegionName { get; set; }

        [Required(ErrorMessage = "Region Code is required")]
        [Display(Name = "Region Code", Prompt = "Region Code")]
        public string RegionCode { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State", Prompt = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address Type is required")]
        [Display(Name = "Address Type")]
        public CustomerAddressTypeEnum? AddressType { get; set; }

        public string AddressId { get; private set; }

        public string ContactId { get; set; }
    }
}