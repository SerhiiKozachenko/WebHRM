using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Hrm.Data;
using NHibernate;

namespace Hrm.Web.Infrastructure.Installers
{
    public class NHibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                //Component.For<ISessionFactory>().LifeStyle.Singleton.Instance(PersistenceManager.Factory),
                Component.For<ISession>().LifeStyle.PerWebRequest.UsingFactoryMethod(x => x.Resolve<ISessionFactory>().OpenSession())
                );
        }
    }
}