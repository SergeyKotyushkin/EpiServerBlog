using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Blocks
{
    [ContentType(DisplayName = "BlogBlock", GUID = "9ab22edd-5d5d-4445-b890-3faa18c87a43", Description = "")]
    public class BlogBlock : BlockData
    {
        [Display(
            Name = "Blog Header",
            Description = "The header of the new blog",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string BlogHeader { get; set; }

        [Display(
            Name = "Blog Author",
            Description = "The author of the new blog",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string BlogAuthor { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Articles Content Area",
            Description = "It allows you to add new articles into the current blog",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea ArticlesContentArea { get; set; }
    }
}