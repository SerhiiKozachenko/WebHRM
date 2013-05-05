using System;
using Hrm.Data.EF.Models.Enums;

namespace Hrm.Web.Models.JobApplication
{
    public class JobApplicationModel
    {
        public long Id { get; set; }

        public long JobId { get; set; }

        public long UserId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime FilingDate { get; set; }

        public int DesiredSalary { get; set; }

        public JobApplicationStatuses Status { get; set; }
    }
}