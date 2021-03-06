using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;

namespace EpiServerBlogs.Web
{

    public class Global
    {
        /// <summary>
        /// Names used for UIHint attributes to map specific rendering controls to page properties
        /// </summary>
        public static class SiteUiHints
        {
            public const string Strings = "StringList";
        }

        /// <summary>
        /// Names used for folders
        /// </summary>
        public static class FolderNames
        {
            public const string Blogs = "Blogs";
        }

        public static string GetVirtualPath(ContentReference pageLink)
        {
            return ServiceLocator.Current.GetInstance<UrlResolver>()
                .GetVirtualPath(pageLink)
                .VirtualPath;
        }
    }
}

