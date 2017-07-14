using System.Collections.Generic;
using System.Linq;
using EpiServerBlogs.Web.Models.Blocks;
using EPiServer;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.Business.SelectionFactories
{
    public class BlockSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var rep = ServiceLocator.Current.GetInstance<IContentRepository>();

            var contentAssetHelper = ServiceLocator.Current.GetInstance<ContentAssetHelper>();
            var startPageFolder = contentAssetHelper.GetOrCreateAssetFolder(ContentReference.StartPage);
            var blogFolders = rep.GetChildren<ContentFolder>(startPageFolder.ContentLink);
            var blogFolder = blogFolders.FirstOrDefault(bf => bf.Name.Equals(Global.FolderNames.Blogs));

            if (blogFolder == null)
                return Enumerable.Empty<ISelectItem>();

            return rep.GetChildren<IContent>(blogFolder.ContentLink)
                .Where(b => b is BlogBlock)
                .Select(
                    b =>
                        new SelectItem
                        {
                            Text = rep.Get<BlogBlock>(b.ContentLink).BlogAuthor + "|" + b.Name,
                            Value = b.ContentLink
                        })
                .ToArray();
        }
    }
}