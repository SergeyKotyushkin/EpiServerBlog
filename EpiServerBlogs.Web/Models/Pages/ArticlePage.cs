using System;
using System.ComponentModel.DataAnnotations;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Pages
{
    [ContentType(DisplayName = "ArticlePage", GUID = "98f3df17-11ce-4dac-bc07-dcb95b155d32", Description = "")]
    public class ArticlePage : SitePageData
    {
        [CultureSpecific]
        [Display(
            Name = "Article Header",
            Description = "This is a header of the current article",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString ArticleHeader { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Article Body",
            Description = "This is a body of the current article",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        public virtual XhtmlString ArticleBody { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Article Date",
            Description = "This is a date of the current article",
            GroupName = SystemTabNames.Content,
            Order = 3)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public virtual DateTime ArticleDateTime { get; set; }

        [CultureSpecific]
        [Display(
            Name = "Article Link",
            Description = "This is a link to the current article",
            GroupName = SystemTabNames.Content,
            Order = 4)]
        public virtual Url ArticleLink { get; set; }
    }
}