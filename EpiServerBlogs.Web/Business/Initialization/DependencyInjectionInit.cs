using System.Web.Mvc;
using EpiServerBlogs.Web.Business.ImageRepository;
using EPiServer.Core.Internal;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.ServiceLocation.Compatibility;

namespace EpiServerBlogs.Web.Business.Initialization
{
    [ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
    [InitializableModule]
    public class DependencyInjectionInit : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.Configure(c => { c.For<ThumbnailManager>().Use<ExtendedThumbnailManager>(); });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(context.Container));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}