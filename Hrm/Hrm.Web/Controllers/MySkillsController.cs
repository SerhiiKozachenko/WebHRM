using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
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
            var mySkillsCats = curUser.UsersSkills.Select(x => x.SkillCategory).Distinct().Select(Mapper.Map<MySkillCategoryModel>);

            return this.Json(new {MySkillsCats = mySkillsCats, TotalCount = mySkillsCats.Count()}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailedRowGridData(GridContext ctx)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var mySkills = curUser.UsersSkills.AsQueryable();
            
            if (ctx.HasFilters)
            {
                mySkills = ctx.ApplyFilters(mySkills);
            }

            return Json(new {Skills = mySkills.Select(Mapper.Map<MySkillModel>), TotalCount = mySkills.Count()}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSkillCategoriesForAdd()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var mySkillsCats = curUser.UsersSkills.Select(x => x.SkillCategory).Distinct();
            var skillCatsForAdd = this.skillCategoriesRepo.ToList().Except(mySkillsCats).Select(x => new KendoDropDownFKModel<long>{value = x.Id, text = x.Name});

            return Json(skillCatsForAdd, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateGridData(MySkillModel model)
        {
            //var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            //var userSkillToUpdate = curUser.UsersSkills.Single(x => x.Id.Equals(model.Id));
            //userSkillToUpdate.Estimate = model.Estimate;
            //this.userSkillsRepo.SaveOrUpdate(userSkillToUpdate);

            var userSkillToUpdate = this.userSkillsRepo.FindOne(new ByIdSpecify<UserSkill>(model.Id));
            userSkillToUpdate.Estimate = model.Estimate;
            this.userSkillsRepo.SaveOrUpdate(userSkillToUpdate);
        }

        [HttpDelete]
        public void DeleteGridData(MySkillCategoryModel model)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var userSkillsToRem = curUser.UsersSkills.Where(x => x.SkillCategory.Id.Equals(model.Id));
            foreach (var skillsToRem in userSkillsToRem)
            {
                this.userSkillsRepo.Delete(skillsToRem);
            }
        }

        [HttpPut]
        public void CreateGridData(long skillsCatId)
        {
            var skillsCat = this.skillCategoriesRepo.FindOne(new ByIdSpecify<SkillCategory>(skillsCatId));
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            foreach (var skill in skillsCat.Skills)
            {
                this.userSkillsRepo.SaveOrUpdate(new UserSkill
                {
                    UserId = curUser.Id,
                    SkillCategoryId = skillsCat.Id,
                    SkillId = skill.Id
                });
            }
        }
    }
}
