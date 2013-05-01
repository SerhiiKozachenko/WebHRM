using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using FluentValidation.Mvc;
using Hrm.Web.App_Start;

namespace Hrm.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            container = WindsorConfig.RegisterIoc(container);
            FluentValidationModelValidatorProvider.Configure();
            AutoMapperConfig.RegisterMappingProfiles();
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}