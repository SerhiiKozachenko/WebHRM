using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Hrm.Core.Entities;
using Hrm.Core.Enums;

namespace Hrm.Data.Mappings.Overrides
{
    public class JobApplicationMappingOverride : IAutoMappingOverride<JobApplication>
    {
        public void Override(AutoMapping<JobApplication> mapping)
        {
            mapping.Table("JobApplications");
            mapping.Map(p => p.Status).CustomType<JobApplicationStatuses>();
        }
    }
}