using System;
using System.Globalization;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.ImageRepository;
using EpiServerBlogs.Web.Business.Services;
using EPiServer.Core.Internal;
using EPiServer.Editor;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.ServiceLocation.Compatibility;
using Mediachase.Commerce;

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
                c.For<ICurrentMarket>().Singleton().Use<CurrentMarket>();

                //Register for auto injection of edit mode check, should be default life cycle (per request)
                c.For<Func<bool>>().Use(() => (() => PageEditing.PageIsInEditMode));
                
                c.For<Func<CultureInfo>>().Use(() => (() => ContentLanguage.PreferredCulture));

                c.For<ThumbnailManager>().Use<ExtendedThumbnailManager>();
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