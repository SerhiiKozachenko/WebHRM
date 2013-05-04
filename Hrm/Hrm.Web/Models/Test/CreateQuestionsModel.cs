using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Web.Models.Test
{
    public class CreateQuestionsModel
    {
        [Required(ErrorMessage = "Обязательное поле!")]
        [Display(Name = "Вопрос")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Обязательное поле!")]
        [Display(Name = "Время на ответ (мин.)")]
        [Range(1, 9, ErrorMessage = "Время от 1 до 9 мин.")]
        [RegularExpression(@"\d{1,1}", ErrorMessage = "Ошибка ввода!")]
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
        [Display(Name = "Правильный")]
        public bool IsCorrect { get; set; }
    }
}