using Hrm.Data.EF.Models.Base;

namespace Hrm.Data.EF.Models
{
    public class UserSkill : BaseModel<long>
    {
        public virtual User User { get; set; }

        public virtual long UserId { get; set; }

        public virtual SkillCategory SkillCategory { get; set; }

        public virtual long SkillCategoryId { get; set; }

        public virtual Skill Skill { get; set; }

        public virtual long SkillId { get; set; }

        public virtual int Estimate { get; set; }
    }
}