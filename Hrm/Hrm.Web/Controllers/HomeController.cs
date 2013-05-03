using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Models.Enums;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;

namespace Hrm.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IRepository<User> usersRepo) : base(usersRepo)
        {
        }

        public ActionResult Index()
        {
            var rep = new Hrm.Data.EF.Repositories.Base.Repository<User>();
            var df = rep.FindOne(new UserByLoginSpecify(User.Identity.Name));


            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            if (curUser.Role.HasFlag(Roles.Manager))
            {
                return RedirectToAction("Index", "Department");
            }

            if (curUser.Role.HasFlag(Roles.Admin))
            {
                return RedirectToAction("Admin");
            }

            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Manager()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
