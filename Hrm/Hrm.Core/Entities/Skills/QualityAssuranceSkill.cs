using Hrm.Core.Entities.Skills.Base;

namespace Hrm.Core.Entities.Skills
{
    public class QualityAssuranceSkill : BaseSkill
    {
        public virtual int IntegrationTesting { get; set; }

        public virtual int AutomationTesting { get;set; }

        public virtual int Documentation { get; set; }
    }
}