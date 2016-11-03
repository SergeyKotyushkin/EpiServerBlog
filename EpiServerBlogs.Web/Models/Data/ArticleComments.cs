using EpiServerBlogs.Web.Models.DynamicData;
using EpiServerBlogs.Web.Models.Pages;

namespace EpiServerBlogs.Web.Models.Data
{
    public class ArticleComments
    {
        public ArticlePage Page { get; set; }
 
        public Comment[] Comments { get; set; } 
    }
}