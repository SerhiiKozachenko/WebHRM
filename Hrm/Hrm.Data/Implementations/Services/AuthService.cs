using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Core.Interfaces.Services;
using Hrm.Data.Implementations.Specifications.Users;
using Roles = Hrm.Core.Enums.Roles;

namespace Hrm.Data.Implementations.Services
{
    public class AuthService : SqlMembershipProvider, IAuthService
    {
        private readonly IRepository<User> usersRepo;

        public AuthService(IRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        #region Implementation of IAuthService

        public string EncryptPassword(string clearPassword)
        {
            return Convert.ToBase64String(base.EncryptPassword(System.Text.Encoding.Unicode.GetBytes(clearPassword)));
        }

        public string DecryptPassword(string encryptedPassword)
        {
            return System.Text.Encoding.Unicode.GetString(base.DecryptPassword(Convert.FromBase64String(encryptedPassword)));
        }

        public bool IsUserExist(string login, string clearPassword)
        {
            var password = this.EncryptPassword(clearPassword);

            return this.usersRepo.FindOne(new UserExistSpecify(login, password)) != null;
        }

        public bool SignIn(string login, string password, bool rememberMe)
        {
            if (this.IsUserExist(login, password))
            {
                this.SignIn(login, rememberMe);
                return true;
            }

            return false;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void Register(User user)
        {
            var encryptedPwd = this.EncryptPassword(user.Password);
            user.Password = encryptedPwd;
            user.IsConfirmed = true;
            user.Role = Roles.User;
            this.usersRepo.SaveOrUpdate(user);
            this.SignIn(user.Login, false);
        }

        public bool IsUserNameExist(string username)
        {
            return this.usersRepo.FindOne(new UserByLoginSpecify(username)) != null;
        }

        public bool IsEmailExist(string email)
        {
            return this.usersRepo.FindOne(new UserByEmailSpecify(email)) != null;
        }

        #endregion

        #region Private

        void SignIn(string login, bool rememberMe)
        {
            var cookieSection = (HttpCookiesSection)ConfigurationManager.GetSection("system.web/httpCookies");
            var authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");

            var issueDate = DateTime.Now;
            var expirationDate = issueDate.AddMinutes(authenticationSection.Forms.Timeout.TotalMinutes);
            var userData = string.Empty;

            var authTicket = new FormsAuthenticationTicket(1, login, issueDate, expirationDate, rememberMe, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authenticateCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (cookieSection.RequireSSL || authenticationSection.Forms.RequireSSL)
            {
                authenticateCookie.Secure = true;
            }

            HttpContext.Current.Response.Cookies.Add(authenticateCookie);
        }

        #endregion
    }
}