﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Job;
using Hrm.Web.Models.JobApplication;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class JobApplicationController : BaseController
    {
        private readonly IRepository<JobApplication> jobAppRepo;

        public JobApplicationController(IRepository<User> usersRepo, IRepository<JobApplication> jobAppRepo)
            : base(usersRepo)
        {
            this.jobAppRepo = jobAppRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<JobApplication> query = this.jobAppRepo;
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

            var jobApplications = query.Skip(ctx.Skip).Take(ctx.Take).Select(Mapper.Map<JobApplicationModel>);

            return Json(new { JobApplications = jobApplications, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }
    }
}