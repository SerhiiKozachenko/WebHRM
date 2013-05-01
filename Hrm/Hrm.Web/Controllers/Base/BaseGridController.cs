using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Entities.Base;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Specifications.Common;
using Hrm.Web.Filters;
using Hrm.Web.Models.Base;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers.Base
{
    public abstract class BaseGridController<TModel, TEntity> : BaseController 
        where TEntity : BaseEntity where TModel : BaseModel
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

            var data = query.Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<TEntity, TModel>);

            return Json(new { Data = data, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
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

        [Transaction]
        [HttpDelete]
        public virtual void DeleteGridData(TModel model)
        {
            if (model != null)
            {
                var entity = this.repo.FindOne(new ByIdSpecify<TEntity>(model.Id));
                this.repo.Delete(entity);
            }
        }

        [Transaction]
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
