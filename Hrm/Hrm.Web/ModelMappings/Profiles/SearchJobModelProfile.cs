using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.SearchJob;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class SearchJobModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Job, SearchJobModel>()
                  .ForMember(dest => dest.ProjectFormalizeNameId,
                             opt => opt.MapFrom(src => src.Project.ProjectFormalizeNameId));

            Mapper.CreateMap<SearchJobModel, Job>();
        }
    }
}