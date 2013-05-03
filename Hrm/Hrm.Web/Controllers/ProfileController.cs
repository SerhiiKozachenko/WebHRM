using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Profile;
using Profile = Hrm.Data.EF.Models.Profile;

namespace Hrm.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IRepository<Profile> profilesRepo;

        public ProfileController(IRepository<User> usersRepo, IRepository<Profile> profilesRepo)
            : base(usersRepo)
        {
            this.profilesRepo = profilesRepo;
        }

        public ActionResult Index()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            Mapper.CreateMap<User, ProfileModel>();
            var model = Mapper.Map<User, ProfileModel>(curUser);

            if (curUser.Profile != null)
            {
                Mapper.CreateMap<Profile, ProfileModel>();
                Mapper.Map<Profile, ProfileModel>(curUser.Profile, model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));
                Mapper.CreateMap<ProfileModel, User>();
                Mapper.Map<ProfileModel, User>(model, curUser);

                if (model.Resume != null)
                {
                    model.ResumePath = this.SaveResumeFile(model.Resume);
                }

                Mapper.CreateMap<ProfileModel, Profile>();
                if (curUser.Profile != null)
                {
                    Mapper.Map<ProfileModel, Profile>(model, curUser.Profile);
                }
                else
                {
                    var profile = new Profile();
                    Mapper.Map<ProfileModel, Profile>(model, profile);
                    curUser.Profile = profile;
                }

                this.profilesRepo.SaveOrUpdate(curUser.Profile);
                this.usersRepo.SaveOrUpdate(curUser);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult RemoveResume()
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            this.RemoveResumeFile(curUser.Profile.ResumePath);
            curUser.Profile.ResumePath = string.Empty;

            this.profilesRepo.SaveOrUpdate(curUser.Profile);

            return RedirectToAction("Index");
        }

        #region Private 

        string SaveResumeFile(HttpPostedFileBase file)
        {
            var resumeFolder = Request.MapPath("~/Content/Resumes");
            if (!Directory.Exists(resumeFolder))
            {
                Directory.CreateDirectory(resumeFolder);
            }

            var fileName = Guid.NewGuid() + file.FileName.Split(new[] { "\\" }, StringSplitOptions.None).LastOrDefault();
            file.SaveAs(Path.Combine(resumeFolder, fileName));

            return fileName;
        }

        void RemoveResumeFile(string fileName)
        {
            var resumeFolder = Request.MapPath("~/Content/Resumes");
            var path = Path.Combine(resumeFolder, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        #endregion
    }
}
