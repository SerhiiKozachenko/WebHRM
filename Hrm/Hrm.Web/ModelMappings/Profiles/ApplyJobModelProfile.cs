using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.SearchJob;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class ApplyJobModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<JobApplication, ApplyJobModel>()
                  .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title))
                  .ForMember(dest => dest.JobDescription, opt => opt.MapFrom(src => src.Job.Description))
                  .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Job.Salary))
                  .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Job.Department.Title))
                  .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Job.Project.Title));

            Mapper.CreateMap<ApplyJobModel, JobApplication>();
        }
    }
}