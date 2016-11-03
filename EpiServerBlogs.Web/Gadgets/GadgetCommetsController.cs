using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.DynamicData;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Shell.Gadgets;
using EPiServer.Shell.Web;
using Newtonsoft.Json;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.Gadgets
{
    [Gadget(ClientScriptInitMethod = "playground.init")]
    [ScriptResource("~/Static/js/gadget-comments.js")]
    [CssResource("~/Static/css/bootstrap.min.css")]
    public class GadgetCommetsController : Controller
    {
        public ActionResult Index()
        {
            var model = new GadgetCommentsViewModel();
            return View(model);
        }

        public string Save(string data)
        {
            if (data == null)
                return "0";

            dynamic values = JsonConvert.DeserializeObject(data);

            var isChecked = values.isChecked.Value == "1";
            int pageId = 0, commentId = 0;

            var rep = ServiceLocator.Current.GetInstance<IContentRepository>();

            if (values.pageId == null || values.commentId == null ||
                !int.TryParse(values.pageId.Value, out pageId) ||
                !int.TryParse(values.commentId.Value, out commentId))
                return "0";
            
            var articles = rep.GetChildren<ArticlePage>(ContentReference.StartPage).ToArray();
            var page = articles.FirstOrDefault(a => a.PageLink.ID == pageId);

            if (page == null)
                return "0";

            var comment = Comment.GetComments(page.PageLink).FirstOrDefault(c => c.Id.StoreId == commentId);

            if (comment == null)
                return "0";

            comment.DoNotShow = isChecked;
            comment.Save();

            return "1";
        }
    }
}