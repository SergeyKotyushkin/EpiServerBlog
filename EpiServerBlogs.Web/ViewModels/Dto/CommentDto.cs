using EpiServerBlogs.Web.Models.DynamicData;

namespace EpiServerBlogs.Web.ViewModels.Dto
{
    public class CommentDto
    {
        public string DateOutput { get; set; }

        public string Username { get; set; }

        public string Text { get; set; }

        public long Order { get; set; }


        public static CommentDto FromComment(Comment comment)
        {
            return new CommentDto
            {
                DateOutput = comment.DateTime.ToString("G"),
                Username = comment.Name,
                Text = comment.Text,
                Order = comment.Id.StoreId
            };
        }
    }
}