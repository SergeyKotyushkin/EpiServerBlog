using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Pages
{
    public abstract class SitePageData : PageData
    {
        [Display(GroupName = "SEO", Order = 100)]
        public virtual String MetaTitle { get; set; }

        [Display(GroupName = "SEO", Order = 200)]
        public virtual String MetaKeywords { get; set; }

        [Display(GroupName = SystemTabNames.Settings, Order = 200)]
        [CultureSpecific]
        public virtual bool Header { get; set; }
    }
}