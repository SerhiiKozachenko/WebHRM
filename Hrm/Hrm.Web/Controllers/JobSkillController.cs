using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.JobSkill;
using KendoWrapper.Grid;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    [Authorize]
    public class JobSkillController : Controller
    {
        private readonly IRepository<JobSkill> jobSkillsRepo;

        private readonly IRepository<SkillCategory> skillCategoriesRepo;

        private readonly IRepository<Job> jobsRepo;

        private long CurrentJobId 
        {
           get { return long.Parse(Session["JobId"].ToString()); }
           set { Session["JobId"] = value; }
        }

        public JobSkillController(IRepository<Job> jobsRepo, IRepository<JobSkill> jobSkillsRepo, IRepository<SkillCategory> skillCategoriesRepo)
        {
            this.jobSkillsRepo = jobSkillsRepo;
            this.skillCategoriesRepo = skillCategoriesRepo;
            this.jobsRepo = jobsRepo;
        }

        public ActionResult Index(long id)
        {
            this.CurrentJobId = id;

            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
            Mapper.CreateMap<SkillCategory, JobSkillCategoryModel>();
            var jobSkillCats = curJob.JobSkills.Select(x => x.SkillCategory).Distinct().Select(Mapper.Map<JobSkillCategoryModel>);

            return this.Json(new { JobSkillCats = jobSkillCats, TotalCount = jobSkillCats.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailedRowGridData(GridContext ctx)
        {
            var curUser = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
            var mySkills = curUser.JobSkills.AsQueryable();

            if (ctx.HasFilters)
            {
                mySkills = ctx.ApplyFilters(mySkills);
            }

            return Json(new { Skills = mySkills.Select(Mapper.Map<JobSkillModel>), TotalCount = mySkills.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSkillCategoriesForAdd()
        {
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
            var mySkillsCats = curJob.JobSkills.Select(x => x.SkillCategory).Distinct().ToList();
            var skillCatsForAdd = this.skillCategoriesRepo.ToList().Except(mySkillsCats).Select(x => new KendoDropDownFKModel<long> { value = x.Id, text = x.Name });

            return Json(skillCatsForAdd, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateGridData(JobSkillModel model)
        {
            var jobSkillToUpdate = this.jobSkillsRepo.FindOne(new ByIdSpecify<JobSkill>(model.Id));
            jobSkillToUpdate.Estimate = model.Estimate;
            this.jobSkillsRepo.SaveOrUpdate(jobSkillToUpdate);
        }

        [HttpDelete]
        public void DeleteGridData(JobSkillCategoryModel model)
        {
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
            var jobsSkillsToRem = curJob.JobSkills.Where(x => x.SkillCategory.Id.Equals(model.Id));
            foreach (var skillsToRem in jobsSkillsToRem)
            {
                this.jobSkillsRepo.Delete(skillsToRem);
            }
        }

        [HttpPut]
        public void CreateGridData(long skillsCatId)
        {
            var skillsCat = this.skillCategoriesRepo.FindOne(new ByIdSpecify<SkillCategory>(skillsCatId));
            var curJob = this.jobsRepo.FindOne(new ByIdSpecify<Job>(this.CurrentJobId));
            foreach (var skill in skillsCat.Skills)
            {
                this.jobSkillsRepo.SaveOrUpdate(new JobSkill
                {
                    JobId = curJob.Id,
                    SkillCategoryId = skillsCat.Id,
                    SkillId = skill.Id
                });
            }
        }
    }
}
