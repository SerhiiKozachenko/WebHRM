using System.Transactions;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Specifications.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Filters;
using Hrm.Web.Models.SkillMatrix;
using Profile = Hrm.Core.Entities.Profile;

namespace Hrm.Web.Controllers
{
    public class SkillMatrixController : BaseController
    {
        private IRepository<SkillMatrix> skillsRepo;

        private IRepository<Profile> profilesRepo;

        public SkillMatrixController(IRepository<User> usersRepo, IRepository<SkillMatrix> skillsRepo, IRepository<Profile> profilesRepo)
            : base(usersRepo)
        {
            this.skillsRepo = skillsRepo;
            this.profilesRepo = profilesRepo;
        }

        public ActionResult Index()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
            var model = new SkillMatrixModel();
            if (curUser.Profile != null && curUser.Profile.SkillMatrix != null)
            {
                Mapper.Map(curUser.Profile.SkillMatrix, model);
            }

            return View(model);
        }

        [HttpPost]
        [Transaction]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SkillMatrixModel model)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            if (curUser.Profile == null)
            {
                curUser.Profile = new Profile(new SkillMatrix());
            }
            else if(curUser.Profile.SkillMatrix == null)
            {
                curUser.Profile.SkillMatrix = new SkillMatrix();
            }

            Mapper.Map(model, curUser.Profile.SkillMatrix);
            this.usersRepo.SaveOrUpdate(curUser);
           
            return RedirectToAction("Index");
        }
    }
}
