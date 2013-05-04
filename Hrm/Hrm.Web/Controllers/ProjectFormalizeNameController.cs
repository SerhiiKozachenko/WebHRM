using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.ProjectFormalizeName;
using KendoWrapper.Grid;

namespace Hrm.Web.Controllers
{
    public class ProjectFormalizeNameController : BaseGridController<ProjectFormalizeNameModel, ProjectFormalizeName>
    {
        public ProjectFormalizeNameController(IRepository<User> usersRepo, IRepository<ProjectFormalizeName> repo) : base(usersRepo, repo)
        {
        }

        public JsonResult GetAllProjectFormalizeNamesFKDropDownModel()
        {
            var model = this.repo.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.FormalizeName });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
