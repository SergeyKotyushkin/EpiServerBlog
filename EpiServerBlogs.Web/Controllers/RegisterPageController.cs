using System;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;

namespace EpiServerBlogs.Web.Controllers
{
    public class RegisterPageController : PageControllerBase<RegisterPage>
    {
        private readonly ISiteAuthService _siteAuthService;

        public RegisterPageController(ISiteCartService siteCartService, ISiteAuthService siteAuthService)
            : base(siteCartService)
        {
            _siteAuthService = siteAuthService;
        }

        public ActionResult Index(RegisterPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = new RegisterPageViewModel(currentPage);
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterPage currentPage, RegisterPagePostViewModel model)
        {
            RegisterPageViewModel errorModel;
            if (!ValidateRegisterForm(currentPage, model, out errorModel))
                return View("Index", errorModel);

            string errorMessage;
            if (!TryRegister(model.UserName.Trim(), model.Password.Trim(), model.Email.Trim(), out errorMessage))
            {
                errorModel.ErrorMessage = errorMessage;
                return View("Index", errorModel);
            }

            return Redirect(Url.ContentUrl(ContentReference.StartPage));
        }


        private bool ValidateRegisterForm(RegisterPage currentPage, RegisterPagePostViewModel model, out RegisterPageViewModel errorModel)
        {
            model.Email = model.Email == null ? null : model.Email.Trim();
            model.UserName = model.UserName == null ? null : model.UserName.Trim();
            model.Password = model.Password == null ? null : model.Password.Trim();
            model.RepeatPassword = model.RepeatPassword == null ? null : model.RepeatPassword.Trim();

            errorModel = new RegisterPageViewModel(currentPage)
            {
                RegisterPagePostViewModel = new RegisterPagePostViewModel
                {
                    Email = model.Email,
                    UserName = model.UserName
                },
                ShowErrorMessage = true
            };

            if (string.IsNullOrWhiteSpace(model.Email) ||
                model.Email.Length < 6 ||
                model.Email.IndexOf("@", StringComparison.Ordinal) == -1)
            {
                errorModel.ErrorMessage = "Invalid email address";
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.UserName) || model.UserName.Length < 3)
            {
                errorModel.ErrorMessage = "Invalid user name address";
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < _siteAuthService.GetMinPasswordLength)
            {
                errorModel.ErrorMessage = "Invalid password address";
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.RepeatPassword) || !model.Password.Equals(model.RepeatPassword))
            {
                errorModel.ErrorMessage = "Passwords are not similar";
                return false;
            }

            return true;
        }

        private bool TryRegister(string username, string password, string email, out string errorMessage)
        {
            var user = _siteAuthService.CreateUser(username, password, email, out errorMessage);

            if (user == null)
                return false;

            if (!_siteAuthService.LogIn(username, password, out errorMessage))
                return false;

            return true;
        }
    }
}