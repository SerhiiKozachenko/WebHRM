using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Hrm.Web.Infrastructure.Plumbing;
using Microsoft.Practices.ServiceLocation;

namespace Hrm.Web
{
    public class WindsorConfig
    {
        public static IWindsorContainer RegisterIoc(IWindsorContainer container)
        {
            container = new WindsorContainer().Install(FromAssembly.This());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            return container;
        }
    }
}