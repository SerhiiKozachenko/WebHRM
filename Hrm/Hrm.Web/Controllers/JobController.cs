using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Models.Enums;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
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

            var jobs = query.OrderBy(x=>x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<JobModel>);

            return Json(new { Jobs = jobs, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

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

        [HttpDelete]
        public void DeleteGridData(JobModel model)
        {
            if (model != null)
            {
                var job = this.jobsRepo.FindOne(new ByIdSpecify<Job>(model.Id));
                this.jobsRepo.Delete(job);
            }
        }

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
