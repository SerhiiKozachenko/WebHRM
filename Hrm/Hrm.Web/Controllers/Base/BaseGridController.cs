using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Models.Base;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.Base;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers.Base
{
    public abstract class BaseGridController<TModel, TEntity> : BaseController
        where TEntity : BaseModel<long>
        where TModel : BaseModel
    {
        protected readonly IRepository<TEntity> repo;

        protected BaseGridController(IRepository<User> usersRepo, IRepository<TEntity> repo)
            : base(usersRepo)
        {
            this.repo = repo;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetGridData(GridContext ctx)
        {
            IQueryable<TEntity> query = this.repo;
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
                        query = this.repo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.repo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            var data = query.OrderBy(x=>x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<TEntity, TModel>);

            return Json(new { Data = data, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual void UpdateGridData(TModel model)
        {
            if (model != null)
            {
                var entity = this.repo.FindOne(new ByIdSpecify<TEntity>(model.Id));
                Mapper.Map(model, entity);

                this.repo.SaveOrUpdate(entity);
            }
        }

        [HttpDelete]
        public virtual void DeleteGridData(TModel model)
        {
            if (model != null)
            {
                var entity = this.repo.FindOne(new ByIdSpecify<TEntity>(model.Id));
                this.repo.Delete(entity);
            }
        }

        [HttpPut]
        public virtual void CreateGridData(TModel model)
        {
            if (model != null)
            {
                var entity = Mapper.Map<TEntity>(model);
                this.repo.SaveOrUpdate(entity);
            }
        }
    }
}
