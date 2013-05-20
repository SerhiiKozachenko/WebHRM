using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Web.Models.Test
{
    public class CreateQuestionsModel
    {
        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Required!")]
        [Display(Name = "Time to answer (min)")]
        [Range(1, 9, ErrorMessage = "Between 1 and 9 minutes")]
        [RegularExpression(@"\d{1,1}", ErrorMessage = "Input error!")]
        public byte TimeToAnswer { get; set; }

        [Required]
        public IList<AnswerModel> Answers { get; set; }
    }

    public class AnswerModel
    {
        [Required(ErrorMessage = "*")]
        [Display()]
        public string Answer { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }
    }
}