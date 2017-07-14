using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EpiServerBlogs.Web.Business.SelectionFactories;
using EpiServerBlogs.Web.Models.Properties;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Models.Pages
{
    [ContentType(DisplayName = "ArticlePage", GUID = "98f3df17-11ce-4dac-bc07-dcb95b155d32", Description = "")]
    public class ArticlePage : SitePageData
    {
        [Required]
        [CultureSpecific]
        [Display(
            Name = "Article Header",
            Description = "This is a header of the current article",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString ArticleHeader { get; set; }

        [Required]
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
            Name = "Article Image",
            Description = "This is an image of the current article",
            GroupName = SystemTabNames.Content,
            Order = 5)]
        [UIHint(UIHint.MediaFile)]
        public virtual ContentReference ArticleImage { get; set; }
        
        [Required]
        [BackingType(typeof(PropertyStrings))]
        [Display(Order = 6)]
        [UIHint(Global.SiteUiHints.Strings)]
        [CultureSpecific]
        public virtual IList<string> ArticleTags { get; set; }

        [Required]
        [CultureSpecific]
        [Display(
            Name = "Parent blog",
            Description = "Choose the blog that is a parent of this article",
            GroupName = SystemTabNames.Content,
            Order = 7)]
        [SelectOne(SelectionFactoryType = typeof(BlockSelectionFactory))]
        public virtual string BlogSelect { get; set; }
    }
}