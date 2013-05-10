using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.JobApplication;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class JobApplicationController : BaseController
    {
        private readonly IRepository<JobApplication> jobAppRepo;

        private readonly IRepository<JobSkill> jobSkillsRepo;

        private readonly IRepository<UserSkill> userSkillsRepo;

        public JobApplicationController(IRepository<User> usersRepo,
            IRepository<JobApplication> jobAppRepo, IRepository<JobSkill> jobSkillsRepo,
            IRepository<UserSkill> userSkillsRepo)
            : base(usersRepo)
        {
            this.jobAppRepo = jobAppRepo;
            this.jobSkillsRepo = jobSkillsRepo;
            this.userSkillsRepo = userSkillsRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<JobApplication> query = this.jobAppRepo.OrderBy(x => x.Id);

            //if (ctx.Filters.Any(f => f.Filter1.Field.Equals("ReqNotLow")))
            //{
            //    var jobId = ctx.Filters.Select(x => x.Filter1).Single(x => x.Field.Equals("JobId")).Value;
            //    query = this.SelectHasAllRequiredSkillsAndNotLower(long.Parse(jobId));
            //    var filter = ctx.Filters.Single(f => f.Filter1.Field.Equals("ReqNotLow"));
            //    ctx.Filters.Remove(filter);
            //}
            //else if (ctx.Filters.Any(f => f.Filter1.Field.Equals("Req")))
            //{
            //    var jobId = ctx.Filters.Select(x => x.Filter1).Single(x => x.Field.Equals("JobId")).Value;
            //    query = this.SelectHasAllRequiredSkills(long.Parse(jobId));
            //    var filter = ctx.Filters.Single(f => f.Filter1.Field.Equals("Req"));
            //    ctx.Filters.Remove(filter);
            //}
            //else
            //{
            //    query = this.jobAppRepo.OrderBy(x => x.Id);
            //}

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
                        query = this.jobAppRepo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.jobAppRepo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            var jobApplications = query.Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<JobApplicationModel>);

            return Json(new { JobApplications = jobApplications, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        //private IQueryable<JobApplication> SelectHasAllRequiredSkillsAndNotLower(long jobId)
        //{
        //    var jobApplications = this.jobAppRepo.Where(x => x.JobId == jobId).ToList();
        //    var requiredSkills = this.jobSkillsRepo.Where(c => c.JobId == jobId);

        //    var selectedJobApplications = new List<JobApplication>();

        //    foreach (var jobApp in jobApplications)
        //    {
        //        var userSkills = jobApp.User.UsersSkills;

        //        foreach (var jobSkill in requiredSkills)
        //        {
        //            if (userSkills.All(x => x.SkillId != jobSkill.SkillId))
        //            {
        //                goto nextWithoutSaving;
        //            }
        //            else if (userSkills.Single(x => x.SkillId == jobSkill.SkillId).Estimate < jobSkill.Estimate)
        //            {
        //                goto nextWithoutSaving;
        //            }
        //        }

        //        selectedJobApplications.Add(jobApp);

        //    nextWithoutSaving:
        //        ;
        //    }

        //    return selectedJobApplications.AsQueryable();
        //}

        //private IQueryable<JobApplication> SelectHasAllRequiredSkills(long jobId)
        //{
        //    var jobApplications = this.jobAppRepo.Where(x => x.JobId == jobId).ToList();
        //    var requiredSkills = this.jobSkillsRepo.Where(c => c.JobId == jobId);

        //    var selectedJobApplications = new List<JobApplication>();

        //    foreach (var jobApp in jobApplications)
        //    {
        //        var userSkills = jobApp.User.UsersSkills;

        //        foreach (var jobSkill in requiredSkills)
        //        {
        //            if (userSkills.All(x => x.SkillId != jobSkill.SkillId))
        //            {
        //                goto nextWithoutSaving;
        //            }
        //        }

        //        selectedJobApplications.Add(jobApp);

        //    nextWithoutSaving:
        //        ;
        //    }

        //    return selectedJobApplications.AsQueryable();
        //}
    }
}
