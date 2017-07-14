using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Blocks;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class BlogBlockController : BlockController<BlogBlock>
    {
        public override ActionResult Index(BlogBlock currentBlock)
        {
            return PartialView(currentBlock);
        }
    }
}
