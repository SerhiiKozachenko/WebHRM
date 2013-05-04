namespace Hrm.Web.Models.Test
{
    public class EditQuestionModel : CreateQuestionsModel
    {
        public long QuestionId { get; set; }

        public long TestId { get; set; }
    }
}