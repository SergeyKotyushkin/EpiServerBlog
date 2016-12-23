namespace EpiServerBlogs.Web.ViewModels.Partial
{
    public class ShoppingCartPartialViewModel
    {
        public string CartName { get; set; }

        public int TotalCount { get; set; }

        public string CartTitle
        {
            get { return string.Format("{0} ({1})", CartName, TotalCount); }
        }
    }
}