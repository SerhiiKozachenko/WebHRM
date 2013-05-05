using System.Collections.Generic;

namespace Hrm.Web.Models.AssignedTest
{
    public class PassTestModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<QuestionTest> Questions { get; set; }
    }

    public class QuestionTest
    {
        public long Id { get; set; }

        public string QuestionValue { get; set; }

        public byte TimeToAnswer { get; set; }

        public List<AnswerTest> Answers { get; set; }
    }

    public class AnswerTest
    {
        public long Id { get; set; }

        public string AnswerValue { get; set; }

        public bool IsChoisen { get; set; }

        public bool IsCorrect { get; set; }
    }
}