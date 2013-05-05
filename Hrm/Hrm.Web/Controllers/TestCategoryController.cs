using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.TestCategory;
using KendoWrapper.Grid;

namespace Hrm.Web.Controllers
{
    public class TestCategoryController : BaseGridController<TestCategoryModel, TestCategory>
    {
        public TestCategoryController(IRepository<User> usersRepo, IRepository<TestCategory> repo) : base(usersRepo, repo)
        {
        }

        public JsonResult GetAllTestCategories()
        {
            var model = base.repo.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllTestsInCategory(long id)
        {
            var model = base.repo.FindOne(new ByIdSpecify<TestCategory>(id)).Tests.Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Name });
             
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
