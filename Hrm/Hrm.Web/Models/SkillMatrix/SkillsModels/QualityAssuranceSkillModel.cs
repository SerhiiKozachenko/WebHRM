namespace Hrm.Web.Models.SkillMatrix.SkillsModels
{
    public class QualityAssuranceSkillModel : BaseSkillModel
    {
        public int IntegrationTesting { get; set; }

        public int AutomationTesting { get; set; }

        public int Documentation { get; set; }
    }
}