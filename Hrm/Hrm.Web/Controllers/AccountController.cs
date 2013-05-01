using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Services;
using Hrm.Web.Models.Account;

namespace Hrm.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService auth;

        public AccountController(IAuthService auth)
        {
            this.auth = auth;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid && auth.SignIn(model.Login, model.Password, model.RememberMe))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOff()
        {
            this.auth.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Mapper.CreateMap<RegisterModel, User>();
                var user = Mapper.Map<RegisterModel, User>(model);
                this.auth.Register(user);

                return RedirectToAction("RegisterSuccess");
            }

            return View(model);
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}
