using System.Collections.Generic;

namespace Hrm.Web.Models.SelectionResult
{
    public class ReportModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int CommandAmount { get; set; }

        public IList<JobReportModel> Jobs { get; set; } 

        public ReportModel()
        {
            this.Jobs = new List<JobReportModel>();
        }
    }

    public class JobReportModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public IList<CandidateReportModel> Candidates { get; set; }

        public JobReportModel()
        {
            this.Candidates = new List<CandidateReportModel>();
        }
    }

    public class CandidateReportModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public bool HasSelected { get; set; }

        public bool HasTested { get; set; }

        public int PercentMatchJobProfile { get; set; }

        public string TestsCompleted { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public string DateOfBirth { get; set; }

        public string LastJobTitle { get; set; }

        public string TotalWorkExperience { get; set; }

        public IList<TestResultModel> TestResults { get; set; }

        public CandidateReportModel()
        {
            this.TestResults = new List<TestResultModel>();
        }
    }
}