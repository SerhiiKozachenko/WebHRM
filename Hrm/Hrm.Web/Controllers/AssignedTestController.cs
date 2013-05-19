using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Data.EF.Specifications.Implementations.Users;
using Hrm.Web.Controllers.Base;
using Hrm.Web.Models.AssignedTest;
using KendoWrapper.Grid.Context;

namespace Hrm.Web.Controllers
{
    public class AssignedTestController : BaseController
    {
        private readonly IRepository<Test> testsRepo;

        private readonly IRepository<TestResult> testsResRepo;

        private readonly IRepository<ResultQuestion> qResRepo;

        private readonly IRepository<ResultAnswer> aResRepo;

        public AssignedTestController(IRepository<User> usersRepo, IRepository<Test> testsRepo,
            IRepository<TestResult> testsResRepo, IRepository<ResultQuestion> qResRepo,
            IRepository<ResultAnswer> aResRepo)
            : base(usersRepo)
        {
            this.testsRepo = testsRepo;
            this.testsResRepo = testsResRepo;
            this.qResRepo = qResRepo;
            this.aResRepo = aResRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetGridData(GridContext ctx)
        {
            var curUser = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name));

            IQueryable<Test> query = curUser.AssignedTests.AsQueryable();
            var totalCount = query.Count();

            if (ctx.HasFilters)
            {
                query = ctx.ApplyFilters(query);
                totalCount = query.Count();
            }

            if (ctx.HasSorting)
            {
                switch (ctx.SortOrder)
                {
                    case SortOrder.Asc:
                        query = this.testsRepo.SortByAsc(ctx.SortColumn, query);
                        break;

                    case SortOrder.Desc:
                        query = this.testsRepo.SortByDesc(ctx.SortColumn, query);
                        break;
                }
            }

            Mapper.CreateMap<AssignedTestModel, Test>().ReverseMap();
            var data = query.OrderBy(x => x.Id).Skip(ctx.Skip).Take(ctx.Take).ToList().Select(Mapper.Map<AssignedTestModel>);

            return Json(new { Data = data, TotalCount = totalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Pass(long id)
        {
            var test = testsRepo.FindOne(new ByIdSpecify<Test>(id));

            var model = new PassTestModel { Id = test.Id, Name = test.Name, Questions = new List<QuestionTest>() };

            foreach (var q in test.Questions)
            {
                model.Questions.Add(new QuestionTest
                {
                    Id = q.Id,
                    QuestionValue = q.QuestionText,
                    TimeToAnswer = q.TimeToAnswer,
                    Answers = new List<AnswerTest>(q.Answers
                        .Select(a => new AnswerTest
                        {
                            Id = a.Id,
                            AnswerValue = a.AnswerText,
                            IsCorrect = a.IsCorrect
                        }).ToList())
                });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult PassResult(PassTestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isValid = false });
            }


            var test = testsRepo.FindOne(new ByIdSpecify<Test>(model.Id));

            var result = new TestResult
                {
                    UserId = this.usersRepo.FindOne(new UserByLoginSpecify(User.Identity.Name)).Id,
                    TestId = test.Id,
                    PassDate = DateTime.Now
                };

            testsResRepo.SaveOrUpdate(result);

            var correctAnswers = 0;

            foreach (var question in model.Questions)
            {
                var resultQuestion = new ResultQuestion
                    {
                        QuestionText = question.QuestionValue,
                        TestResultId = result.Id
                    };

                qResRepo.SaveOrUpdate(resultQuestion);

                foreach (var answer in question.Answers)
                {
                    if (answer.IsCorrect && answer.IsChoisen)
                        correctAnswers++;
                    var resultAnswer = new ResultAnswer
                        {
                            AnswerText = answer.AnswerValue, 
                            ResultQuestionId = resultQuestion.Id,
                            IsCorrect = answer.IsCorrect,
                            IsChoisen = answer.IsChoisen
                        };

                    aResRepo.SaveOrUpdate(resultAnswer);

                    //resultQuestion.ResultAnswers.Add(resultAnswer);
                }

                //result.ResultQuestions.Add(resultQuestion);
            }

            ViewBag.correctAnswers = correctAnswers;
            ViewBag.totalQuestios = model.Questions.Count;
            return View();
        }
    }
}
