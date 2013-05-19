using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Skill;
using KendoWrapper.Grid;

namespace Hrm.Web.Controllers
{
    public class SkillController : BaseGridController<SkillModel, Skill>
    {
        public SkillController(IRepository<User> usersRepo, IRepository<Skill> repo) : base(usersRepo, repo)
        {
        }

        public JsonResult GetAllSkills()
        {
            var model = base.repo.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
