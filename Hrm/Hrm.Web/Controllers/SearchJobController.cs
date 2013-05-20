using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Models.Enums;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.SearchJob;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class SearchJobController : BaseController
    {
        private readonly IRepository<Job> jobsRepo;

        private readonly IRepository<JobApplication> jobsAppRepo;

        public SearchJobController(IRepository<Job> jobsRepo, IRepository<User> usersRepo, IRepository<JobApplication> jobsAppRepo)
            : base(usersRepo)
        {
            this.jobsRepo = jobsRepo;
            this.jobsAppRepo = jobsAppRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<Job> query;
            if (ctx.Filters.Any(x => x.Filter1.Field.Equals("ProjectFormalizeNameId")))
            {
                var projectFormalizeNameId = long.Parse(ctx.Filters.Select(x => x.Filter1).Single(c => c.Field.Equals("ProjectFormalizeNameId")).Value);
                query = this.jobsRepo.Where(x => x.Project.ProjectFormalizeNameId == projectFormalizeNameId);
                var filter = ctx.Filters.Single(f => f.Filter1.Field.Equals("ProjectFormalizeNameId"));
                ctx.Filters.Remove(filter);
            }
            else
            {
               query = this.jobsRepo;
            }

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

            var jobs = query.OrderBy(x => x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<SearchJobModel>);

            return Json(new { Jobs = jobs, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ApplyJob(long id)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var job = this.jobsRepo.FindOne(new ByIdSpecify<Job>(id));
            var model = new ApplyJobModel
                {
                    JobId = id,
                    UserId = curUser.Id,
                    Department = job.Department.Title,
                    Project = job.Project.ProjectFormalizeName.FormalizeName,
                    Salary = job.Salary,
                    JobDescription = job.Description,
                    JobTitle = job.Title
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult ApplyJob(ApplyJobModel model)
        {
            var jobApplication = Mapper.Map<JobApplication>(model);
            jobApplication.FilingDate = DateTime.Now;
            jobApplication.InterviewResult = InterviewResults.UnderConsideration;
            this.jobsAppRepo.SaveOrUpdate(jobApplication);

            return RedirectToAction("Index");
        }
    }
}
