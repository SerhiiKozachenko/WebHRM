using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;

namespace Hrm.Data.Mappings.Overrides
{
    public class JobMappingOverride : IAutoMappingOverride<Job>
    {
        public void Override(AutoMapping<Job> mapping)
        {
            mapping.Table("Jobs");
            mapping.HasMany(m => m.Skills).Table("JobsSkills");
        }
    }
}