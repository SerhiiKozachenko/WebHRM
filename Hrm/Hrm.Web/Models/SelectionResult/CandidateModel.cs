using System;
using Hrm.Data.EF.Models.Enums;
using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.SelectionResult
{
    public class CandidateModel : BaseModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public bool HasSelected { get; set; }

        public bool HasTested { get; set; }

        public bool HasInterviewed { get; set; }

        public InterviewResults? InterviewResult { get; set; }

        public string InterviewComment { get; set; }

        public int PercentMatchJobProfile { get; set; }

        public int Variance { get; set; }

        public string TestsCompleted { get; set; }

        public long JobId { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public string DateOfBirth { get; set; }

        public string LastJobTitle { get; set; }

        public string TotalWorkExperience { get; set; }

        public string ResumePath { get; set; }
    }
}