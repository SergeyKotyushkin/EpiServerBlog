using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Models.Pages
{
    public abstract class SitePageData : PageData
    {
        [Required]
        [Display(GroupName = "SEO", Order = 100)]
        public virtual String MetaTitle { get; set; }

        [Display(GroupName = "SEO", Order = 200)]
        public virtual String MetaKeywords { get; set; }

        [CultureSpecific]
        [Display(GroupName = SystemTabNames.Content, Order = 200)]
        [UIHint(UIHint.Textarea)]
        public virtual XhtmlString Header { get; set; }
    }
}