using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Skill;

namespace Hrm.Web.Controllers
{
    public class SkillController : BaseGridController<SkillModel, Skill>
    {
        public SkillController(IRepository<User> usersRepo, IRepository<Skill> repo) : base(usersRepo, repo)
        {
        }
    }
}
