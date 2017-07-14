using System;
using System.Linq;
using EPiServer.Core;
using EPiServer.Data;
using EPiServer.Data.Dynamic;

namespace EpiServerBlogs.Web.Models.DynamicData
{
    [EPiServerDataStore(AutomaticallyRemapStore = true)]
    public class Comment : IDynamicData
    {
        public Identity Id { get; set; }

        public DateTime DateTime { get; set; }

        public int PageId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public bool Checked { get; set; }

        public bool DoNotShow { get; set; }

        
        protected void Initialize()
        {
            Id = Identity.NewIdentity(Guid.NewGuid());
            DateTime = DateTime.Now;

            Name = string.Empty;
            Text = string.Empty;
        }

        public Comment()
        {
            Initialize();
        }

        public Comment(int pageId, string name, string text)
        {
            PageId = pageId;
            Name = name;
            Text = text;
            DateTime = DateTime.Now;
        }

        /// <summary>
        /// Saves comment to Dynamic Data Store
        /// </summary>
        public void Save()
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(Comment));
            store.Save(this);
        }

        /// <summary>
        /// Delete the comment permanently
        /// </summary>
        /// <param name="comment"></param>
        public static void Delete(Comment comment)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(Comment));
            store.Delete(comment.Id);
        }

        /// <summary>
        /// Get all comments for page
        /// </summary>
        public static Comment[] GetComments(PageReference pageLink)
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(Comment));
            var comments = store.Items<Comment>().Where(x => x.PageId == pageLink.ID);
            return comments.ToArray();
        }

        /// <summary>
        /// Get unchecked comments
        /// </summary>
        public static Comment[] GetUncheckedComments()
        {
            var store = DynamicDataStoreFactory.Instance.CreateStore(typeof(Comment));
            var comments = store.Items<Comment>().Where(x => !x.Checked);
            return comments.ToArray();
        }
    }
}