using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.Project;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class ProjectModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Project, ProjectModel>()
                  .ForMember(dest => dest.ProjectFormalizeNameId, opt => opt.MapFrom(src => src.ProjectFormalizeName.Id));

            Mapper.CreateMap<ProjectModel, Project>();
        }
    }
}