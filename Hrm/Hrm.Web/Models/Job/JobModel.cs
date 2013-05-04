namespace Hrm.Web.Models.Job
{
    public class JobModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Salary { get; set; }

        public long DepartmentId { get; set; }

        public long ProjectId { get; set; }
    }
}