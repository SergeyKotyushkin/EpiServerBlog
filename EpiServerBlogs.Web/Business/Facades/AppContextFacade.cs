using System;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Core;

namespace EpiServerBlogs.Web.Business.Facades
{
    [ServiceConfiguration(typeof(AppContextFacade), Lifecycle = ServiceInstanceScope.Singleton)]
    public class AppContextFacade
    {
        public virtual Guid ApplicationId
        {
            get { return AppContext.Current.ApplicationId; }
        }
    }
}