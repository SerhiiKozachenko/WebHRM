using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace Hrm.Web.Filters
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        private ITransaction transaction;

        public bool RollbackOnModelStateError { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.transaction = ServiceLocator.Current.GetInstance<ISession>().BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (((filterContext.Exception != null) && (filterContext.ExceptionHandled)) || this.ShouldRollback(filterContext))
            {
                this.transaction.Rollback();
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            this.transaction = ServiceLocator.Current.GetInstance<ISession>().BeginTransaction();
           
            try
            {
                if (((filterContext.Exception != null) && (!filterContext.ExceptionHandled)) || this.ShouldRollback(filterContext))
                {
                    this.transaction.Rollback();
                }
                else
                {
                    this.transaction.Commit();
                }
            }
            finally
            {
                this.transaction.Dispose();
            }
        }

        private bool ShouldRollback(ControllerContext filterContext)
        {
            return this.RollbackOnModelStateError && !filterContext.Controller.ViewData.ModelState.IsValid;
        }
    }
}