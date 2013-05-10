using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.Selection;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class JobApplicationToCandidateModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<JobApplication, CandidateModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ReverseMap();
        }
    }
}