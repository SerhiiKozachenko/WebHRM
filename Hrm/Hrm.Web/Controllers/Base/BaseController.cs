using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;

namespace Hrm.Web.Controllers.Base
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IRepository<User> usersRepo;

        public BaseController(IRepository<User> usersRepo)
        {
            this.usersRepo = usersRepo;
        }
    }
}