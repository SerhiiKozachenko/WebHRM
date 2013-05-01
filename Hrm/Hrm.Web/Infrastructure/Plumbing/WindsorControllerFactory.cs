using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace Hrm.Web.Infrastructure.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Container Kernel 
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorControllerFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            this.kernel.ReleaseComponent(controller);
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">
        /// The context of the HTTP request, which includes the HTTP context and route data.
        /// </param>
        /// <param name="controllerType">
        /// The type of the controller.
        /// </param>
        /// <returns>
        /// The controller instance.
        /// </returns>
        /// <exception cref="T:System.Web.HttpException">
        /// <paramref name="controllerType"/>is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="controllerType"/>cannot be assigned.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// An instance of<paramref name="controllerType"/>cannot be created.
        /// </exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return (IController)this.kernel.Resolve(controllerType);
        }
    }
}