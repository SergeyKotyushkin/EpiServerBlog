namespace EpiServerBlogs.Web.Business.Services.Contracts
{
    public interface ISiteAuthService
    {
        bool LogIn(string username, string password, out string errorMessage);

        void LogOut();
    }
}