using Hrm.Core.Entities.Skills.Base;

namespace Hrm.Core.Entities.Skills
{
    public class DesignSkill : BaseSkill
    {
        public virtual int ThreeD { get; set; }

        public virtual int TwoD { get; set; }

        public virtual int Typography { get; set; }

        public virtual int WebDesign { get; set; }

        public virtual int Photography { get; set; }
    }
}