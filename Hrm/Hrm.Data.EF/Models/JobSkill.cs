using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class JobSkill : BaseModel<long>
    {
        public virtual Job Job { get; set; }

        public virtual long JobId { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual long SkillCategoryId { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual long SkillId { get; set; }

        public virtual int Estimate { get; set; } 
    }
}