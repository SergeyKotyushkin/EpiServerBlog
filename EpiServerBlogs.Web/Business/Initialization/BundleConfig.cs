using System.Web.Optimization;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EpiServerBlogs.Web.Business.Initialization
{
    [InitializableModule]
    public class BundleConfig : IInitializableModule
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Static/js/jquery-2.2.3.min.js", //jquery.js can be removed and linked from CDN instead, we use a local one for demo purposes without internet connectionzz
                        "~/Static/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Static/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Static/css/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Static/css/style.css", new CssRewriteUrlTransform()));
        }

        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.WebApplication)
            {
                RegisterBundles(BundleTable.Bundles);
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}