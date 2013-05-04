using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Project;

namespace Hrm.Web.Controllers
{
    public class ProjectController : BaseGridController<ProjectModel, Project>
    {
        public ProjectController(IRepository<User> usersRepo, IRepository<Project> repo) : base(usersRepo, repo)
        {
        }
    }
}
