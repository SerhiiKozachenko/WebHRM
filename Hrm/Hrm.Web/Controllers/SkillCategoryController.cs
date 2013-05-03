using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.SkillCategory;
using KendoWrapper.Grid;

namespace Hrm.Web.Controllers
{
    public class SkillCategoryController : BaseGridController<SkillCategoryModel, SkillCategory>
    {
        public SkillCategoryController(IRepository<User> usersRepo, IRepository<SkillCategory> repo) : base(usersRepo, repo)
        {
        }

        public JsonResult GetAllSkillCategories()
        {
            var model = base.repo.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
