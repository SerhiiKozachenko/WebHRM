using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.Selection;

namespace Hrm.Web.Controllers
{
    public class SelectionController : Controller
    {
        private readonly IRepository<Job> jobsRepo;

        private readonly IRepository<User> usersRepo; 

        private long CurrentJobId
        {
            get { return long.Parse(Session["JobId"].ToString()); }
            set { Session["JobId"] = value; }
        }

        public SelectionController(IRepository<Job> jobsRepo, IRepository<User> usersRepo)
        {
            this.jobsRepo = jobsRepo;
            this.usersRepo = usersRepo;
        }

        public ActionResult Index(int id)
        {
            this.CurrentJobId = id;

            return View();
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
                            CategoryAvgEstimate = (double)skillCat.Sum(x => x.Estimate) / skillCat.Count()
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
        }

        public ActionResult TestAssigning()
        {
            return null;
        }
    }
}
