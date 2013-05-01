using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Enums;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Specifications.Common;
using Hrm.Data.Implementations.Specifications.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Filters;
using Hrm.Web.Models.Job;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class JobController : BaseController
    {
        private readonly IRepository<Job> jobsRepo;

        public JobController(IRepository<User> usersRepo, IRepository<Job> jobsRepo)
            : base(usersRepo)
        {
            this.jobsRepo = jobsRepo;
        }

        public ActionResult Index()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            if (curUser.Role.HasFlag(Roles.Manager))
            {
                return View("ManageJobs");
            }

            return View("ViewDepartments");
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<Job> query = this.jobsRepo;
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
                        query = this.jobsRepo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.jobsRepo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            var jobs = query.Skip(ctx.Skip).Take(ctx.Take).Select(Mapper.Map<JobModel>);

            return Json(new { Jobs = jobs, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        [HttpPost]
        public void UpdateGridData(JobModel model)
        {
            if (model != null)
            {
                var job = this.jobsRepo.FindOne(new ByIdSpecify<Job>(model.Id));
                Mapper.Map(model, job);

                this.jobsRepo.SaveOrUpdate(job);
            }
        }

        [Transaction]
        [HttpDelete]
        public void DeleteGridData(JobModel model)
        {
            if (model != null)
            {
                var job = this.jobsRepo.FindOne(new ByIdSpecify<Job>(model.Id));
                this.jobsRepo.Delete(job);
            }
        }

        [Transaction]
        [HttpPut]
        public void CreateGridData(JobModel model)
        {
            if (model != null)
            {
                var job = Mapper.Map<Job>(model);
                this.jobsRepo.SaveOrUpdate(job);
            }
        }
    }
}
