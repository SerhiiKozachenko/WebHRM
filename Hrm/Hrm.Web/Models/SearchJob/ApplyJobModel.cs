namespace Hrm.Web.Models.SearchJob
{
    public class ApplyJobModel
    {
        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public string Project { get; set; }

        public long JobId { get; set; }

        public long UserId { get; set; }

        public int DesiredSalary { get; set; }
    }
}