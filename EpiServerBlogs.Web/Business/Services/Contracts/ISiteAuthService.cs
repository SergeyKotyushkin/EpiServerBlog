using System.Web.Security;

namespace EpiServerBlogs.Web.Business.Services.Contracts
{
    public interface ISiteAuthService
    {
        bool LogIn(string username, string password, out string errorMessage);

        void LogOut();

        bool CanCreateUser(string email, string username, out string errorMessage);

        MembershipUser CreateUser(string username, string password, string email, out string errorMessage);

        int GetMinPasswordLength { get; }
    }
}