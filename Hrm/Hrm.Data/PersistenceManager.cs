using System.IO;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Diagnostics;
using Hrm.Core.Entities;
using Hrm.Core.Entities.Base;
using Hrm.Core.Entities.Skills;
using Hrm.Core.Entities.Skills.Base;
using Hrm.Data.Mappings.Overrides;
using NHibernate;
using NHibernate.Cfg;

namespace Hrm.Data
{
    public class PersistenceManager
    {
        private static readonly string SCHEMA_EXPORT_PATH = Path.Combine(@"D:\Diplom\Project\WebHRM\WebHRM\Hrm", "HBM-XML-MAPPINGS");

        private static ISessionFactory factory;

        public static ISessionFactory Factory
        {
            get { return factory ?? (factory = CreateConfiguration().BuildSessionFactory()); }
        }

        public static ISession Session
        {
            get 
            { 
                var session = Factory.OpenSession();
                session.FlushMode = FlushMode.Commit;
                return session;
            }
        }

        private static Configuration CreateConfiguration()
        {
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("hrm")))
                .Mappings(m => m.AutoMappings
                                   .Add(AutoMap.AssemblyOf<User>(c => c.BaseType == typeof(BaseEntity))
                                            .IgnoreBase<BaseEntity>()
                                            .UseOverridesFromAssemblyOf<UserMappingOverride>())
                                   .Add(AutoMap.AssemblyOf<ProgrammingSkill>(c => c.BaseType == typeof(BaseSkill))
                                            .IgnoreBase<BaseSkill>()
                                            .UseOverridesFromAssemblyOf<UserMappingOverride>())
                                    //.Add(AutoMap.AssemblyOf<JobSkills>().UseOverridesFromAssemblyOf<JobSkillsMappingOverride>)
                                    //.Add(AutoMap.AssemblyOf<UserSkills>().UseOverridesFromAssemblyOf<UserSkillsMappingOverride>)
                                    .ExportTo(SCHEMA_EXPORT_PATH))
                .BuildConfiguration();

            return cfg;
        }
    }
}