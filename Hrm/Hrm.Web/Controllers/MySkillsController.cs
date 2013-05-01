using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Specifications.Common;
using Hrm.Data.Implementations.Specifications.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Filters;
using Hrm.Web.Models.MySkills;
using KendoWrapper.Grid;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class MySkillsController : BaseController
    {
        private readonly IRepository<UserSkill> userSkillsRepo;

        private readonly IRepository<SkillCategory> skillCategoriesRepo;

        public MySkillsController(IRepository<User> usersRepo, IRepository<UserSkill> userSkillsRepo, IRepository<SkillCategory> skillCategoriesRepo)
            : base(usersRepo)
        {
            this.userSkillsRepo = userSkillsRepo;
            this.skillCategoriesRepo = skillCategoriesRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            Mapper.CreateMap<SkillCategory, MySkillCategoryModel>();
            var mySkillsCats = curUser.Skills.Select(x => x.SkillCategory).Distinct().Select(Mapper.Map<MySkillCategoryModel>);

            return this.Json(new {MySkillsCats = mySkillsCats, TotalCount = mySkillsCats.Count()}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailedRowGridData(GridContext ctx)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var mySkills = curUser.Skills.AsQueryable();
            
            if (ctx.HasFilters)
            {
                mySkills = ctx.ApplyFilters(mySkills);
            }

            return Json(new {Skills = mySkills.Select(Mapper.Map<MySkillModel>), TotalCount = mySkills.Count()}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSkillCategoriesForAdd()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var mySkillsCats = curUser.Skills.Select(x => x.SkillCategory).Distinct();
            var skillCatsForAdd = this.skillCategoriesRepo.ToList().Except(mySkillsCats).Select(x => new KendoDropDownFKModel{value = x.Id, text = x.CategoryName});

            return Json(skillCatsForAdd, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        [HttpPost]
        public void UpdateGridData(MySkillModel model)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var userSkillToUpdate = curUser.Skills.Single(x => x.Id.Equals(model.Id));
            userSkillToUpdate.Estimate = model.Estimate;
            this.userSkillsRepo.SaveOrUpdate(userSkillToUpdate);
        }

        [Transaction]
        [HttpDelete]
        public void DeleteGridData(MySkillCategoryModel model)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var userSkillsToRem = curUser.Skills.Where(x => x.SkillCategory.Id.Equals(model.Id));
            foreach (var skillsToRem in userSkillsToRem)
            {
                this.userSkillsRepo.Delete(skillsToRem);
            }
        }

        [Transaction]
        [HttpPut]
        public void CreateGridData(long skillsCatId)
        {
            var skillsCat = this.skillCategoriesRepo.FindOne(new ByIdSpecify<SkillCategory>(skillsCatId));
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            foreach (var skill in skillsCat.Skills)
            {
                this.userSkillsRepo.SaveOrUpdate(new UserSkill
                {
                    User = curUser,
                    SkillCategory = skillsCat,
                    Skill = skill
                });
            }
        }
    }
}
