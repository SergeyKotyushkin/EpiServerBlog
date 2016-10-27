using EpiServerBlogs.Logic.ImageRepository;
using EPiServer.Core.Internal;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace EpiServerBlogs.Web.Business.StructureMap
{
    public class AllRegistry : Registry
    {
        public AllRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            // Currency
            For<ThumbnailManager>().Use<ExtendedThumbnailManager>();
        }
    }
}