using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Antlr.Runtime.Misc;
using EpiServerBlogs.Web.Models.Catalog;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.Business.Extensions
{
    public static class SiteProductContentExtensions
    {
#pragma warning disable 649
        private static Injected<IRelationRepository> _relationRepository;
        private static Injected<IContentLoader> _contentLoader;
#pragma warning restore 649

        public static IEnumerable<SiteVariationContent> GetVariations(this SiteProductContent productContent)
        {
            return _contentLoader.Service
                .GetItems(productContent.GetVariants(_relationRepository.Service), ContentLanguage.PreferredCulture)
                .Cast<SiteVariationContent>();
        } 
    }
}