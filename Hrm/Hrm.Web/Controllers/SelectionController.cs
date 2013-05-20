using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.JobApplication;
using Hrm.Web.Models.Selection;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class SelectionController : Controller
    {
        private readonly IRepository<Job> jobsRepo;

        private readonly IRepository<User> usersRepo;

        private readonly IRepository<JobApplication> jobAppRepo;

        private readonly IRepository<JobSkill> jobSkillsRepo; 

        private long CurrentJobId
        {
            get { return long.Parse(Session["JobId"].ToString()); }
            set { Session["JobId"] = value; }
        }

        private IList<long> CurrentSelectedCandidatesIds
        {
            get { return Session["SelectedCandidatesIds"] as List<long>; }
            set { Session["SelectedCandidatesIds"] = value; }
        } 

        public SelectionController(IRepository<Job> jobsRepo, IRepository<User> usersRepo, IRepository<JobApplication> jobAppRepo, IRepository<JobSkill> jobSkillsRepo)
        {
            this.jobsRepo = jobsRepo;
            this.usersRepo = usersRepo;
            this.jobAppRepo = jobAppRepo;
            this.jobSkillsRepo = jobSkillsRepo;
        }

        public ActionResult Index(int id)
        {
            this.CurrentJobId = id;
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(id));
            ViewBag.JobName = curJob.Title;
            ViewBag.Department = curJob.Department.Title;
            ViewBag.ProjectName = curJob.Project.Title;
            ViewBag.Salary = curJob.Salary;

            return View();
        }

        public JsonResult GetCandidatesGridData(GridContext ctx)
        {
            IQueryable<JobApplication> query;

            if (ctx.Filters.Any(f => f.Filter1.Field.Equals("ReqNotLow")))
            {
                var jobId = ctx.Filters.Select(x => x.Filter1).Single(x => x.Field.Equals("JobId")).Value;
                query = this.SelectHasAllRequiredSkillsAndNotLower(long.Parse(jobId));
                var filter = ctx.Filters.Single(f => f.Filter1.Field.Equals("ReqNotLow"));
                ctx.Filters.Remove(filter);
            }
            else if (ctx.Filters.Any(f => f.Filter1.Field.Equals("Req")))
            {
                var jobId = ctx.Filters.Select(x => x.Filter1).Single(x => x.Field.Equals("JobId")).Value;
                query = this.SelectHasAllRequiredSkills(long.Parse(jobId));
                var filter = ctx.Filters.Single(f => f.Filter1.Field.Equals("Req"));
                ctx.Filters.Remove(filter);
            }
            else
            {
                query = this.jobAppRepo.OrderBy(x => x.Id);
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
                        query = this.jobAppRepo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.jobAppRepo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            var jobApplications = query.Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<CandidateModel>).ToList();

            foreach (var jobApp in jobApplications)
            {
                // Calculate PERCENT MATCH JOB PROFILE SKILLS
                var job = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
                
                var jobSkills = job.JobSkills;
                var userSkills = this.usersRepo.FindOne(new ByIdSpecify<User>(jobApp.UserId)).UsersSkills;
                foreach (var jobSkill in jobSkills)
                {
                    int userEsitmate = 0;
                    if (userSkills.Any(x => x.SkillId == jobSkill.SkillId))
                    {
                        userEsitmate = userSkills.Single(x => x.SkillId == jobSkill.SkillId).Estimate;
                    }

                    jobApp.PercentMatchJobProfile += ((double)userEsitmate / 10 - (double)jobSkill.Estimate / 10) * (double)jobSkill.Estimate / 10;
                    jobApp.Variance += Math.Pow((double)userEsitmate / 10, 2) - Math.Pow((double)jobSkill.Estimate / 10, 2);
                }

                jobApp.Variance = Math.Round(jobApp.Variance, 2);
                //var totalJobSkillEst = jobSkills.Sum(x => x.Estimate);
                //var candPercentage = jobApp.PercentMatchJobProfile * 100 / totalJobSkillEst;
                //jobApp.PercentMatchJobProfile = (candPercentage < 0) ? 100 - Math.Abs(candPercentage) : candPercentage;

            }

            return Json(new { JobApplications = jobApplications.OrderByDescending(x=>x.PercentMatchJobProfile), TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private IQueryable<JobApplication> SelectHasAllRequiredSkillsAndNotLower(long jobId)
        {
            var jobApplications = this.jobAppRepo.Where(x => x.JobId == jobId).ToList();
            var requiredSkills = this.jobSkillsRepo.Where(c => c.JobId == jobId);

            var selectedJobApplications = new List<JobApplication>();

            foreach (var jobApp in jobApplications)
            {
                var userSkills = jobApp.User.UsersSkills;

                foreach (var jobSkill in requiredSkills)
                {
                    if (userSkills.All(x => x.SkillId != jobSkill.SkillId))
                    {
                        goto nextWithoutSaving;
                    }
                    else if (userSkills.Single(x => x.SkillId == jobSkill.SkillId).Estimate < jobSkill.Estimate)
                    {
                        goto nextWithoutSaving;
                    }
                }

                selectedJobApplications.Add(jobApp);

            nextWithoutSaving:
                ;
            }

            return selectedJobApplications.AsQueryable();
        }

        [NonAction]
        private IQueryable<JobApplication> SelectHasAllRequiredSkills(long jobId)
        {
            var jobApplications = this.jobAppRepo.Where(x => x.JobId == jobId).ToList();
            var requiredSkills = this.jobSkillsRepo.Where(c => c.JobId == jobId);

            var selectedJobApplications = new List<JobApplication>();

            foreach (var jobApp in jobApplications)
            {
                var userSkills = jobApp.User.UsersSkills;

                foreach (var jobSkill in requiredSkills)
                {
                    if (userSkills.All(x => x.SkillId != jobSkill.SkillId))
                    {
                        goto nextWithoutSaving;
                    }
                }

                selectedJobApplications.Add(jobApp);

            nextWithoutSaving:
                ;
            }

            return selectedJobApplications.AsQueryable();
        }

        public JsonResult GetJobProfileByCategoriesChartData()
        {
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));

            var skillCatNames = curJob.JobSkills.Select(x => x.SkillCategory).Distinct().Select(x => x.Name);

            var skillCatGroupedData = curJob.JobSkills.GroupBy(g => g.SkillCategoryId);

            var skillsData = new List<ChartSeriesModel>();

            foreach (var skillCat in skillCatGroupedData)
            {
                if (skillCat.Any())
                {
                    var skillData = new ChartSeriesModel
                        {
                            Name = skillCat.First().SkillCategory.Name,
                            CategoryAvgEstimate = Math.Round((double)skillCat.Sum(x => x.Estimate) / skillCat.Count(), 2)
                        };

                    foreach (var jobSkill in skillCat)
                    {
                        skillData.Categories.Add(jobSkill.Skill.Name);
                        skillData.Data.Add(jobSkill.Estimate);
                    }

                    skillsData.Add(skillData);
                }
            }

            return this.Json(
                    new { SkillCatNames = skillCatNames, SkillsData = skillsData },
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobProfileBySkillsChartData()
        {
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));

            var skillNames = curJob.JobSkills.Select(x => x.Skill).Select(x => x.Name);

            var skillsData = new List<ChartSeriesModel>();

            skillsData.Add(new ChartSeriesModel
                {
                    Name = "Job Profile",
                    Data = curJob.JobSkills.Select(x => double.Parse(x.Estimate.ToString())).ToList()
                });

            return this.Json(new { SkillNames = skillNames, SkillsData = skillsData }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SaveSelectedCandidates(IList<long> selected)
        {
            using (var ctx = new HrmContext())
            {
                var curJob = ctx.Jobs.Single(x => x.Id == this.CurrentJobId);
                foreach (var userId in selected)
                {
                    curJob.SelectedCandidates.Add(ctx.Users.Single(x=>x.Id == userId));
                }

                ctx.SaveChanges();
            }

            this.CurrentSelectedCandidatesIds = selected;
        }

        public ActionResult TestAssigning()
        {
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<User> query = this.usersRepo.Where(x => this.CurrentSelectedCandidatesIds.Contains(x.Id));
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
                        query = this.usersRepo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.usersRepo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            var data = query.OrderBy(x => x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<User, SelectedCandidateModel>);

            return Json(new { Data = data, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssignedTests(GridContext ctx)
        {
            var userId = ctx.Filters.Select(x => x.Filter1).Single(x => x.Field.Equals("UserId")).Value;
            var user = this.usersRepo.FindOne(new ByIdSpecify<User>(long.Parse(userId)));

            var data = user.AssignedTests.Select(Mapper.Map<Test, AssignedTestModel>);

            return Json(new {Data = data, TotalCount = data.Count()}, JsonRequestBehavior.AllowGet);
        }

        public void AssignTestToUser(long testId, long userId)
        {
            using (var ctx = new HrmContext())
            {
                var user = ctx.Users.Single(x => x.Id == userId);
                var test = ctx.Tests.Single(x => x.Id == testId);
                user.AssignedTests.Add(test);

                ctx.SaveChanges();
            }
        }
    }
}
