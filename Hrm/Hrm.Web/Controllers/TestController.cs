using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.Test;

namespace Hrm.Web.Controllers
{
    public class TestController : BaseGridController<TestModel, Test>
    {
        private readonly IRepository<Question> qRepo;

        private readonly IRepository<Answer> aRepo;

        public TestController(IRepository<User> usersRepo, IRepository<Test> repo, IRepository<Question> qRepo, IRepository<Answer> aRepo)
            : base(usersRepo, repo)
        {
            this.qRepo = qRepo;
            this.aRepo = aRepo;
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var model = this.repo.FindOne(new ByIdSpecify<Test>(id));

            return View(model);
        }


        [HttpPost]
        public JsonResult ChangeNameDesc(ChangeNameDescModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { isValid = false });

            var test = this.repo.FindOne(new ByIdSpecify<Test>(model.Id));

            test.Name = model.Name;
            test.Description = model.Description;

            this.repo.SaveOrUpdate(test);

            return Json(new { isValid = true });
        }

        [HttpPost]
        public ActionResult EditQuestion(long id)
        {
            var question = this.qRepo.FindOne(new ByIdSpecify<Question>(id));

            //setup our model
            var model = new EditQuestionModel { TestId = question.Test.Id, QuestionId = question.Id, Question = question.QuestionText, TimeToAnswer = question.TimeToAnswer, Answers = new List<AnswerModel>() };
            foreach (var answer in question.Answers)
            {
                model.Answers.Add(new AnswerModel { Answer = answer.AnswerText, IsCorrect = answer.IsCorrect });
            }


            return PartialView("_PartialEditQuestionForm", model);
        }

        [HttpPost]
        public ActionResult RemoveQuestion(long id)
        {
            var questionToDel = this.qRepo.FindOne(new ByIdSpecify<Question>(id));
            this.qRepo.Delete(questionToDel);

            var test = this.repo.FindOne(new ByIdSpecify<Test>(questionToDel.TestId));

            //test.Questions.Remove(questionToDel);

            return PartialView("_PartialChangedTest", test);
        }

        [HttpPost]
        public ActionResult ChangeQuestion(EditQuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isValid = false });
            }

            //var test = _testsRepository.Get(model.TestId);

            //Question to change
            var oldQuestion = this.qRepo.FindOne(new ByIdSpecify<Question>(model.QuestionId));

            oldQuestion.QuestionText = model.Question;
            oldQuestion.TimeToAnswer = model.TimeToAnswer;

            //removing old answer
            var answersToRemove = oldQuestion.Answers.ToList();

            foreach (var ans in answersToRemove)
            {
                var answer = this.aRepo.FindOne(new ByIdSpecify<Answer>(ans.Id));
                this.aRepo.Delete(answer);
                //_answersRepository.Delete(answer);
            }
            //_questionsRepository.SaveOrUpdate(oldQuestion);

            //adding new answers
            foreach (var ans in model.Answers)
            {
                var answer = new Answer
                    {
                        AnswerText = ans.Answer,
                        IsCorrect = ans.IsCorrect,
                        QuestionId = oldQuestion.Id
                    };

                this.aRepo.SaveOrUpdate(answer);
            }

            this.qRepo.SaveOrUpdate(oldQuestion);

            //_testsRepository.SaveOrUpdate(test);

            var test = this.repo.FindOne(new ByIdSpecify<Test>(model.TestId));
            return PartialView("_PartialChangedTest", test);
        }

        [HttpPost]
        public ActionResult AddQuestionEdit()
        {
            var model = new CreateQuestionsModel { Answers = new List<AnswerModel> { new AnswerModel { Answer = "Незнаю" } } };

            return PartialView("_PartialAddQuestionEditForm", model);
        }

        [HttpPost]
        public ActionResult SaveAddQuestionEdit(CreateQuestionsModel model, long testId)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { isValid = false });
            }

            //Saving new question and answers to test
            var test = this.repo.FindOne(new ByIdSpecify<Test>(testId));

            var question = new Question
            {
                QuestionText = model.Question,
                TimeToAnswer = model.TimeToAnswer, 
                TestId = test.Id
            };

            //test.Questions.Add(question);

            this.qRepo.SaveOrUpdate(question);

            foreach (var ans in model.Answers)
            {
                var answer = new Answer
                    {
                        AnswerText = ans.Answer,
                        IsCorrect = ans.IsCorrect,
                        QuestionId = question.Id
                    };
                this.aRepo.SaveOrUpdate(answer);
            }

            //this.qRepo.SaveOrUpdate(question);


            return PartialView("_PartialChangedTest", test);
        }
    }
}
