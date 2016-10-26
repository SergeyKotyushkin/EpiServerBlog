using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Pages
{
    [ContentType(DisplayName = "StartPage", GUID = "82353600-82f4-4965-bc27-94a2866433a2", Description = "")]
    public class StartPage : SitePageData
    {
        [CultureSpecific]
        [Display(
            Name = "Blog Content Area",
            Description = "It allows you to add new blog",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual ContentArea BlogBlockContentArea { get; set; }
    }
}