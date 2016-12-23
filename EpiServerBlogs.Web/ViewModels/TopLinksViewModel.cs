using EPiServer.Core;

namespace EpiServerBlogs.Web.ViewModels
{
    public class TopLinksViewModel
    {
        public bool IsAuthenticated { get; set; } 
        
        public string UserName { get; set; }

        public string ReturnUrl { get; set; }

        public string Language { get; set; }

        public PageReference LoginPageLink { get; set; }

        public bool IsLoginPage { get; set; }
    }
}