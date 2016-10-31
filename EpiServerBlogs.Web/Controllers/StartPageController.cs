using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Blocks;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var updatedCurrentPage = AddNewBlogBlocks(currentPage);

            var model = SitePageViewModel.Create(updatedCurrentPage);
            return View(model);
        }

        private static StartPage AddNewBlogBlocks(PageData currentPage)
        {
            var rep = ServiceLocator.Current.GetInstance<IContentRepository>();
            var writableClonePage = (StartPage) currentPage.CreateWritableClone();

            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            var pageFolder = contentAssetHelper.GetOrCreateAssetFolder(currentPage.ContentLink);
            var blogFolders = rep.GetChildren<ContentFolder>(pageFolder.ContentLink);
            var blogFolder = blogFolders.FirstOrDefault(bf => bf.Name.Equals(Global.FolderNames.Blogs));

            // if folder does not exist then create it
            if (blogFolder == null)
            {
                var folder = rep.GetDefault<ContentFolder>(pageFolder.ContentLink);
                folder.Name = Global.FolderNames.Blogs;

                var folderRef = rep.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);
                blogFolder = rep.Get<ContentFolder>(folderRef);
            }

            // get all added blog blocks
            var currentPageBlogBlocks = rep.GetChildren<IContent>(blogFolder.ContentLink)
                .Select(b => new {Block = rep.Get<BlogBlock>(b.ContentLink), b.ContentLink})
                .Where(b => b != null && b.Block != null)
                .ToArray();

            if (writableClonePage.BlogBlockContentArea == null)
                writableClonePage.BlogBlockContentArea = new ContentArea();

            // get all blog blocks that exist
            var alreadyAddedBlogBlocks = writableClonePage.BlogBlockContentArea.Items
                .Where(b => b.ContentLink.ID != 0)
                .Select(b => new {Block = rep.Get<BlogBlock>(b.ContentLink), b.ContentLink})
                .Where(b => b != null && b.Block != null)
                .ToArray();

            // add new blog blocks if they does not exist already
            foreach (var blog in currentPageBlogBlocks
                .Where(
                    blog =>
                        !alreadyAddedBlogBlocks.Any(
                            b =>
                                b.Block.BlogAuthor == blog.Block.BlogAuthor &&
                                b.Block.BlogHeader == blog.Block.BlogHeader)))
                writableClonePage.BlogBlockContentArea.Items.Add(new ContentAreaItem {ContentLink = blog.ContentLink});

            rep.Save(writableClonePage, SaveAction.Publish, AccessLevel.NoAccess);
            return writableClonePage;
        }
    }
}