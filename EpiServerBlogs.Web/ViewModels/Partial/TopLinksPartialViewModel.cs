using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels.Partial
{
    public class TopLinksPartialViewModel
    {
        public bool IsAuthenticated { get; set; } 
        
        public string UserName { get; set; }

        public string ReturnUrl { get; set; }

        public string Language { get; set; }

        public PageReference LoginPageLink { get; set; }

        public PageReference AccountPageLink { get; set; }
        
        public bool IsLoginPage { get; set; }
    }
}