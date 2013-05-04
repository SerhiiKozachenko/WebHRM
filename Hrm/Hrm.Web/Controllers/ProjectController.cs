using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Project;
using KendoWrapper.Grid;

namespace Hrm.Web.Controllers
{
    public class ProjectController : BaseGridController<ProjectModel, Project>
    {
        public ProjectController(IRepository<User> usersRepo, IRepository<Project> repo) : base(usersRepo, repo)
        {
        }

        public JsonResult GetAllProjectsFKDropDownModel()
        {
            var model = this.repo.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Title });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
