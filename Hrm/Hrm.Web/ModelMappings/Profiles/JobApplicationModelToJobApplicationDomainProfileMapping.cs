using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.JobApplication;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class JobApplicationModelToJobApplicationDomainProfileMapping : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<JobApplication, JobApplicationModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
        }
    }
}