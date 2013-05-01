using System.Web.Mvc;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;

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