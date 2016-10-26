using EpiServerBlogs.Web.Models.Pages;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class PageControllerBase<T> : PageController<T> where T : SitePageData
    {
        
    }
}