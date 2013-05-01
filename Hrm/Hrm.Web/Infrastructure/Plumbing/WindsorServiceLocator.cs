using System;
using System.Collections.Generic;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace Hrm.Web.Infrastructure.Plumbing
{
    public class WindsorServiceLocator : ServiceLocatorImplBase
    {
        /// <summary>
        /// Windsor container
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorServiceLocator"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorServiceLocator(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        ///             When implemented by inheriting classes, this method will do the actual work of resolving
        ///             the requested service instance.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>
        /// The requested service instance.
        /// </returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key != null ? this.container.Resolve(key, serviceType) : this.container.Resolve(serviceType);
        }

        /// <summary>
        ///             When implemented by inheriting classes, this method will do the actual work of
        ///             resolving all the requested service instances.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>
        /// Sequence of service instance objects.
        /// </returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (object[])this.container.ResolveAll(serviceType);
        }
    }
}