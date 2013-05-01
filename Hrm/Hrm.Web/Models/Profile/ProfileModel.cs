using System;
using System.Web;
using DataAnnotationsExtensions;

namespace Hrm.Web.Models.Profile
{
    public class ProfileModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string LastJobTitle { get; set; }

        public int TotalWorkExperience { get; set; }

        [FileExtensions("doc, docx",ErrorMessage = "Only word documents is allowed")]
        public HttpPostedFileBase Resume { get; set; }

        public string ResumePath { get; set; }
    }
}