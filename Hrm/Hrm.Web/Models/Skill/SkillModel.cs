using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.Skill
{
    public class SkillModel : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long SkillCategoryId { get; set; }
    }
}