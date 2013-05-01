using Hrm.Web.Models.SkillMatrix.SkillsModels;

namespace Hrm.Web.Models.SkillMatrix
{
    public class SkillMatrixModel
    {
        public bool HasLanguageSkills { get; set; }

        public bool HasManagementSkills { get; set; }

        public bool HasProgrammingSkills { get; set; }

        public bool HasDesignSkills { get; set; }

        public bool HasQualityAssuranceSkills { get; set; }

        public LanguageSkillModel LanguageSkills { get; set; }

        public ManagementSkillModel ManagementSkills { get; set; }

        public ProgrammingSkillModel ProgrammingSkills { get; set; }

        public DesignSkillModel DesignSkills { get; set; }

        public QualityAssuranceSkillModel QualityAssuranceSkills { get; set; }
    }
}