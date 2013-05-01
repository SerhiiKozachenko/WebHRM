using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class ProfileMappingOverride : IAutoMappingOverride<Profile>
    {
        public void Override(AutoMapping<Profile> mapping)
        {
            mapping.Table("Profiles");
            mapping.References(x => x.SkillMatrix).LazyLoad().Cascade.All();
        }
    }
}