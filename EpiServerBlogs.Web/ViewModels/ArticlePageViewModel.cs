using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels.Dto;

namespace EpiServerBlogs.Web.ViewModels
{
    public class ArticlePageViewModel : SitePageViewModel<ArticlePage>
    {
        public CommentDto[] Comments { get; set; }

        public ArticlePageViewModel(ArticlePage currentPage) : base(currentPage)
        {
        }
    }
}