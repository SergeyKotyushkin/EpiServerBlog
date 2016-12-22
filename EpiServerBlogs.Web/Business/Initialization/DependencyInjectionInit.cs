using System.Web.Mvc;
using EpiServerBlogs.Web.Business.ImageRepository;
using EpiServerBlogs.Web.Business.Services;
using EpiServerBlogs.Web.Business.Services.Contracts;
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
            context.Services.Configure(c =>
            {
                c.For<ThumbnailManager>().Use<ExtendedThumbnailManager>();
                c.For<ISiteCartService>().Use<SiteCartService>();
                c.For<ISitePriceService>().Use<SitePriceService>();
            });

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