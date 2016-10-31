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
            var allArticles = rep.GetChildren<IContent>(ContentReference.StartPage)
                .Where(a => a is ArticlePage)
                .Select(a => new {Article = rep.Get<ArticlePage>(a.ContentLink), a.ContentLink})
                .Where(a => a != null && a.Article != null)
                .ToArray();

            // get all added blog blocks
            var currentPageBlogBlocks = rep.GetChildren<IContent>(blogFolder.ContentLink)
                .Where(b => b is BlogBlock)
                .Select(b => new { Block = rep.Get<BlogBlock>(b.ContentLink), b.ContentLink, b.Name })
                .Where(b => b != null && b.Block != null)
                .ToArray();

            if (writableClonePage.BlogBlockContentArea == null)
                writableClonePage.BlogBlockContentArea = new ContentArea();

            // get all blog blocks that exist
            var alreadyAddedBlogBlocks = writableClonePage.BlogBlockContentArea.Items
                .Select(b => new {Block = rep.Get<BlogBlock>(b.ContentLink), b.ContentLink})
                .Where(b => b != null && b.Block != null)
                .ToArray();

            // add new blog blocks if they does not exist already
            foreach (var blog in currentPageBlogBlocks)
            {
                if (!alreadyAddedBlogBlocks.Any(
                    b => b.Block.BlogAuthor == blog.Block.BlogAuthor && b.Block.BlogHeader == blog.Block.BlogHeader))
                    writableClonePage.BlogBlockContentArea.Items.Add(
                        new ContentAreaItem { ContentLink = blog.ContentLink });
                
                // create writable blog block to update articles inside it
                var writableBlogBlock = rep.Get<BlogBlock>(blog.ContentLink);
                var writableBlogBlockClone = (BlogBlock) writableBlogBlock.CreateWritableClone();

                if (writableBlogBlockClone == null)
                    continue;

                var blogArticles =
                    allArticles.Where(a => a.Article.BlogSelect.Equals(blog.ContentLink.ID.ToString())).ToArray();

                if (writableBlogBlockClone.ArticlesContentArea == null)
                    writableBlogBlockClone.ArticlesContentArea = new ContentArea();

                // clear all and add again
                writableBlogBlockClone.ArticlesContentArea.Items.Clear();
                foreach (var article in blogArticles)
                    writableBlogBlockClone.ArticlesContentArea.Items.Add(
                        new ContentAreaItem {ContentLink = article.ContentLink});

                rep.Save((IContent) writableBlogBlockClone, SaveAction.Publish, AccessLevel.NoAccess);
            }

            rep.Save(writableClonePage, SaveAction.Publish, AccessLevel.NoAccess);
            return writableClonePage;
        }
    }
}