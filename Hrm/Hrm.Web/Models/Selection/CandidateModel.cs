using System;
using Hrm.Data.EF.Models.Enums;
using Hrm.Web.Models.Base;

namespace Hrm.Web.Models.Selection
{
    public class CandidateModel : BaseModel
    {
        public long UserId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime FilingDate { get; set; }

        public int DesiredSalary { get; set; }

        public JobApplicationStatuses Status { get; set; }

        public double PercentMatchJobProfile { get; set; }

        public double Variance { get; set; }
    }
}