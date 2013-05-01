using Hrm.Core.Entities.Skills.Base;

namespace Hrm.Core.Entities.Skills
{
    public class LanguageSkill : BaseSkill
    {
        public virtual int English { get; set; }

        public virtual int French { get; set; }

        public virtual int German { get; set; }

        public virtual int Chinese { get; set; }
    }
}