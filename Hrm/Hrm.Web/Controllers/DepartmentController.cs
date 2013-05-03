using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Models.Enums;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Department;
using KendoWrapper.Grid;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IRepository<Department> departmentsRepo;

        public DepartmentController(IRepository<User> usersRepo, IRepository<Department> departmentsRepo)
            : base(usersRepo)
        {
            this.departmentsRepo = departmentsRepo;
        }

        public ActionResult Index()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            if (curUser.Role.HasFlag(Roles.Manager))
            {
                return View("ManageDepartments");
            }

            return View("ViewDepartments");
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<Department> query = this.departmentsRepo;
            var totalCount = query.Count();

            if (ctx.HasFilters)
            {
                query = ctx.ApplyFilters(query);
                totalCount = query.Count();
            }

            if (ctx.HasSorting)
            {
                switch (ctx.SortOrder)
                {
                    case SortOrder.Asc:
                        query = this.departmentsRepo.SortByAsc(ctx.SortColumn);
                        break;

                    case SortOrder.Desc:
                        query = this.departmentsRepo.SortByDesc(ctx.SortColumn);
                        break;
                }
            }

            var departments = query.OrderBy(x=>x.Id).Skip(ctx.Skip).Take(ctx.Take).Select(Mapper.Map<DepartmentModel>);

            return Json(new { Departments = departments, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateGridData(DepartmentModel model)
        {
            if (model != null)
            {
                var dep = this.departmentsRepo.FindOne(new ByIdSpecify<Department>(model.Id));
                Mapper.Map(model, dep);

                this.departmentsRepo.SaveOrUpdate(dep);
            }
        }

        [HttpDelete]
        public void DeleteGridData(DepartmentModel model)
        {
            if (model != null)
            {
                var dep = this.departmentsRepo.FindOne(new ByIdSpecify<Department>(model.Id));
                this.departmentsRepo.Delete(dep);
            }
        }

        [HttpPut]
        public void CreateGridData(DepartmentModel model)
        {
            if (model != null)
            {
                var dep = Mapper.Map<Department>(model);
                this.departmentsRepo.SaveOrUpdate(dep);
            }
        }

        public JsonResult GetAllDepartmentsFKDropDownModel()
        {
            var model = this.departmentsRepo.Select(x => new KendoDropDownFKModel<long> {value = x.Id, text = x.Title});

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
