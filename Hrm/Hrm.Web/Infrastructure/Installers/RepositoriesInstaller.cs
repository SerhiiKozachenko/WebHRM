using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Repositories.Base;

namespace Hrm.Web.Infrastructure.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        #region Implementation of IWindsorInstaller

        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param><param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)).LifeStyle.Transient);
        }

        #endregion
    }
}