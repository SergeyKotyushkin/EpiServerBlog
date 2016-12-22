using System;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Customers;

namespace EpiServerBlogs.Web.Business.Facades
{
    [ServiceConfiguration(typeof(CustomerContextFacade), Lifecycle = ServiceInstanceScope.Singleton)]
    public class CustomerContextFacade
    {
        public CustomerContextFacade()
        {
            CurrentContact = new CurrentContactFacade();
        }
        public virtual CurrentContactFacade CurrentContact { get; private set; }
        public virtual Guid CurrentContactId { get { return CurrentContact.CurrentContactId; } }
        public virtual CustomerContact GetContactById(Guid contactId)
        {
            return CurrentContact.GetContactById(contactId);
        }
    }
}