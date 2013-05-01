using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hrm.Core.Interfaces.Services;
using Hrm.Data.Implementations.Services;

namespace Hrm.Web.Infrastructure.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        #region Implementation of IWindsorInstaller

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAuthService>().ImplementedBy<AuthService>().LifestylePerWebRequest());
        }

        #endregion
    }
}