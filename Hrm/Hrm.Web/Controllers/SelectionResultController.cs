using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Selection;
using Hrm.Web.Models.SelectionResult;
using KendoWrapper.Grid;
using KendoWrapper.Grid.Context;
using RazorPDF;
using CandidateModel = Hrm.Web.Models.SelectionResult.CandidateModel;

namespace Hrm.Web.Controllers
{
    public class SelectionResultController : BaseController
    {
        private readonly IRepository<JobApplication> jobAppRepo;

        private readonly IRepository<Job> jobsRepo;

        private readonly IRepository<Project> projRepo;

        private readonly IRepository<TestResult> testResRepo;

        public SelectionResultController(IRepository<User> usersRepo, IRepository<JobApplication> jobAppRepo, 
            IRepository<Job> jobsRepo, IRepository<Project> projRepo, IRepository<TestResult> testResRepo) 
            : base(usersRepo)
        {
            this.jobAppRepo = jobAppRepo;
            this.jobsRepo = jobsRepo;
            this.projRepo = projRepo;
            this.testResRepo = testResRepo;
        }

        private long CurrentProjectId
        {
            get { return long.Parse(Session["ProjectId"].ToString()); }
            set { Session["ProjectId"] = value; }
        }

        // Selection result for Project Id
        public ActionResult Index(long id)
        {
            this.CurrentProjectId = id;
            var curProj = this.projRepo.FindOne(new ByIdSpecify<Project>(id));
            ViewBag.ProjectTitle = curProj.Title;
            ViewBag.ProjectDesc = curProj.Description;
            ViewBag.ProjectStartDate = curProj.StartDate.ToShortDateString();
            ViewBag.ProjectEndDate = curProj.EndDate.ToShortDateString();
            ViewBag.ProjectTeamCount = 8;
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<User> query = this.usersRepo.Where(x => x.JobApplications.Any(a=>a.Job.ProjectId == this.CurrentProjectId));
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

            var users = query.OrderBy(x => x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList();

            var data = new List<CandidateModel>();
            foreach (var user in users)
            {
                var candidate = Mapper.Map<CandidateModel>(user);
                // HasSelected
                if (user.Jobs.Any(x => x.ProjectId == this.CurrentProjectId))
                {
                    candidate.HasSelected = true;
                }
                else // Not Selected
                {
                    candidate.HasSelected = false;
                }
                // Has Tested
                if (user.TestResults.Any(x=>user.AssignedTests.Any(a=>a.Id == x.TestId)))
                {
                    candidate.HasTested = true;
                }
                else //Not Tested
                {
                    candidate.HasTested = false;
                }

                // Interview INFO
                var jobApp = user.JobApplications.First(x => x.Job.ProjectId == this.CurrentProjectId);
                candidate.HasInterviewed = jobApp.HasInterviewed;
                candidate.InterviewResult = jobApp.InterviewResult;
                candidate.InterviewComment = jobApp.InterviewComment;
                // Calculate PERCENT MATCH JOB PROFILE SKILLS
                var job = jobApp.Job;
                candidate.JobId = job.Id;
                var jobSkills = job.JobSkills;
                var userSkills = user.UsersSkills;
                int usersSkillSum = 0;
                var jobSkillSum = 0;
                foreach (var jobSkill in jobSkills)
                {
                    int userEsitmate = 0;
                    if (userSkills.Any(x => x.SkillId == jobSkill.SkillId))
                    {
                        userEsitmate = userSkills.Single(x => x.SkillId == jobSkill.SkillId).Estimate;
                    }

                    candidate.PercentMatchJobProfile += ((double)userEsitmate / 10 - (double)jobSkill.Estimate / 10) * (double)jobSkill.Estimate / 10;
                    //candidate.Variance += Math.Pow((double)userEsitmate / 10, 2) - Math.Pow((double)jobSkill.Estimate / 10, 2);
                    candidate.Variance += Math.Pow((double)userEsitmate / 10 - (double)jobSkill.Estimate / 10, 2);
                    usersSkillSum += userEsitmate;
                    jobSkillSum += jobSkill.Estimate;
                }

                candidate.PercentMatchJobProfile = 1 + candidate.PercentMatchJobProfile;
                candidate.Variance = Math.Round(candidate.Variance / (jobSkillSum), 2);
                //var totalJobSkillEst = jobSkills.Sum(x => x.Estimate);
                //var candPercentage = candidate.PercentMatchJobProfile * 100 / totalJobSkillEst;
                //candidate.PercentMatchJobProfile = (candPercentage<0) ? 100 -  Math.Abs(candPercentage) : candPercentage;

                // Calculate TESTS RESULTS
                var assignedTests = user.AssignedTests;
                var testCompleted = assignedTests.Count(assignedTest => user.TestResults.Any(x => x.TestId == assignedTest.Id));
                candidate.TestsCompleted = string.Format("{0} of {1}", testCompleted, assignedTests.Count);

                data.Add(candidate);
            }

            return Json(new { Data = data.OrderByDescending(x=>x.PercentMatchJobProfile), TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        public void UpdateGridData(CandidateModel model)
        {
            var user = this.usersRepo.FindOne(new ByIdSpecify<User>(model.Id));
            var jobAppId = user.JobApplications.First(x => x.Job.ProjectId == this.CurrentProjectId).Id;
            var jobApp = this.jobAppRepo.FindOne(new ByIdSpecify<JobApplication>(jobAppId));
            jobApp.HasInterviewed = model.HasInterviewed;
            jobApp.InterviewResult = model.InterviewResult;
            jobApp.InterviewComment = model.InterviewComment;
            this.jobAppRepo.SaveOrUpdate(jobApp);
        }

        public JsonResult GetCurrentProjectJobsDropDownModel()
        {
            var model = this.jobsRepo.Where(x=>x.ProjectId == this.CurrentProjectId).Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Title });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobProfileSkillsChartData(long? jobId, long? candidateId)
        {
            var skillsData = new List<ChartSeriesModel>();
            var skillNames = new List<string>();
            Job curJob = null;

            if (jobId.HasValue)
            {
                curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(jobId.Value));

                skillNames.AddRange(curJob.JobSkills.Where(x=>x.Estimate > 0).Select(x => x.Skill).Select(x => x.Name));

                skillsData.Add(new ChartSeriesModel
                    {
                        Name = "Job Profile",
                        Data = curJob.JobSkills.Where(x=>x.Estimate > 0).Select(x => double.Parse(x.Estimate.ToString())).ToList(),
                        YAxis = 0
                    });
            }

            if (candidateId.HasValue)
            {
                var candidate = this.usersRepo.FindOne(new ByIdSpecify<User>(candidateId.Value));

                var seriesName = string.Format("{0} {1} Profile", candidate.LastName, candidate.FirstName);

                var userSkills = new List<UserSkill>();
                if (curJob != null)
                {
                    userSkills = candidate.UsersSkills.Where(x => curJob.JobSkills.Any(a => a.SkillId == x.SkillId && a.Estimate > 0)).ToList();
                }
                else
                {
                    skillNames.AddRange(candidate.UsersSkills.Select(x => x.Skill).Select(x => x.Name));
                    userSkills = candidate.UsersSkills.ToList();
                }

                skillsData.Add(new ChartSeriesModel
                {
                    Name = seriesName,
                    Data = userSkills.Select(x => double.Parse(x.Estimate.ToString())).ToList(),
                    YAxis = 0
                });

                // Select TestResults for candidate skills
                var testResults = candidate.TestResults.Where(x => userSkills.Any(s => s.SkillId == x.Test.SkillId)).ToList();

                var skillTestRes = new ChartSeriesModel
                    {
                        Name = "Test Results",
                        YAxis = 1
                    };
                foreach (var skill in userSkills)
                {
                    var testRes = testResults.FirstOrDefault(x => x.Test.SkillId == skill.SkillId);
                    if (testRes != null)
                    {
                        // Percent correct answers
                        var totalQuestions = testRes.Test.Questions.Count;
                        var correctAnswers = testRes.ResultQuestions.Count(x => x.ResultAnswers.Any(a => a.IsCorrect && a.IsChoisen));
                        skillTestRes.Data.Add(Math.Round((double)correctAnswers/totalQuestions*100, 2));
                    }
                    else
                    {
                        skillTestRes.Data.Add(0);
                    }
                }

                skillsData.Add(skillTestRes);
            }

            return this.Json(new { SkillNames = skillNames, SkillsData = skillsData }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTestResultsForCandidate(long id)
        {
            var candidate = this.usersRepo.FindOne(new ByIdSpecify<User>(id));

            var assignedTests = candidate.AssignedTests;
            var passedTestsResults = candidate.TestResults.Where(x => assignedTests.Any(a => a.Id == x.TestId));

            var model = new List<TestResultModel>();
            
            foreach (var testRes in passedTestsResults)
            {
                var totalQuestions = testRes.Test.Questions.Count;
                var correctAnswers = testRes.ResultQuestions.Count(x => x.ResultAnswers.Any(a => a.IsCorrect && a.IsChoisen));
                var res = new TestResultModel
                    {
                        Name = testRes.Test.Name,
                        Category = testRes.Test.Category.Name,
                        PercentCorrectAnswers = (Math.Round((double)correctAnswers/totalQuestions*100, 2)).ToString(),
                        Result = string.Format("{0} of {1}", correctAnswers, totalQuestions)
                    };
                model.Add(res);
            }
            
            return Json(new {Data = model, TotalCount = model.Count}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {
            var model = new ReportModel();
            var curProj = this.projRepo.FindOne(new ByIdSpecify<Project>(this.CurrentProjectId));
            model.Title = curProj.Title;
            model.Description = curProj.Description;
            model.CommandAmount = curProj.Jobs.Count;
            foreach (var job in curProj.Jobs)
            {
                var jobRepModel = new JobReportModel
                    {
                        Title = job.Title,
                        Description = job.Description,
                        Salary = job.Salary,
                        Department = job.Department.Title
                    };

                foreach (var jobApp in job.JobApplications)
                {
                    var canRepModel = new CandidateReportModel
                        {
                            LastName = jobApp.User.LastName,
                            FirstName = jobApp.User.FirstName,
                            MiddleName = jobApp.User.MiddleName,
                            HasSelected = jobApp.User.Jobs.Any(x => x.ProjectId == this.CurrentProjectId),
                            HasTested = jobApp.User.TestResults.Any(x=>jobApp.User.AssignedTests.Any(a=>a.Id == x.TestId))
                        };
                           
                    // Calculate PERCENT MATCH JOB PROFILE SKILLS
                    
                    var jobSkills = job.JobSkills;
                    var userSkills = jobApp.User.UsersSkills;
                    foreach (var jobSkill in jobSkills)
                    {
                        int userEsitmate = 0;
                        if (userSkills.Any(x => x.SkillId == jobSkill.SkillId))
                        {
                            userEsitmate = userSkills.Single(x => x.SkillId == jobSkill.SkillId).Estimate;
                        }

                        canRepModel.PercentMatchJobProfile += (userEsitmate - jobSkill.Estimate);
                    }

                    var totalJobSkillEst = jobSkills.Sum(x => x.Estimate);
                    var candPercentage = canRepModel.PercentMatchJobProfile * 100 / totalJobSkillEst;
                    canRepModel.PercentMatchJobProfile = (candPercentage < 0) ? 100 - Math.Abs(candPercentage) : candPercentage;

                    // Calculate TESTS RESULTS
                    var assignedTests = jobApp.User.AssignedTests;
                    var testCompleted = assignedTests.Count(assignedTest => jobApp.User.TestResults.Any(x => x.TestId == assignedTest.Id));
                    canRepModel.TestsCompleted = string.Format("{0} of {1}", testCompleted, assignedTests.Count);

                    // Test Results Details
                    var passedTestsResults = jobApp.User.TestResults.Where(x => assignedTests.Any(a => a.Id == x.TestId));

                    foreach (var testRes in passedTestsResults)
                    {
                        var totalQuestions = testRes.Test.Questions.Count;
                        var correctAnswers = testRes.ResultQuestions.Count(x => x.ResultAnswers.Any(a => a.IsCorrect && a.IsChoisen));
                        var res = new TestResultModel
                        {
                            Name = testRes.Test.Name,
                            Category = testRes.Test.Category.Name,
                            PercentCorrectAnswers = (Math.Round((double)correctAnswers / totalQuestions * 100, 2)).ToString(),
                            Result = string.Format("{0} of {1}", correctAnswers, totalQuestions)
                        };

                        canRepModel.TestResults.Add(res);
                    }

                    jobRepModel.Candidates.Add(canRepModel);    
                }

                model.Jobs.Add(jobRepModel);
             }

            return new PdfResult(model);
        }
    }
}
