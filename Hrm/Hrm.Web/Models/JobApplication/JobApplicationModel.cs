using System;
using Hrm.Core.Enums;

namespace Hrm.Web.Models.JobApplication
{
    public class JobApplicationModel
    {
        public long Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime FilingDate { get; set; }

        public int? DesiredSalary { get; set; }

        public JobApplicationStatuses Status { get; set; }
    }
}