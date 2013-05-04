using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.ProjectFormalizeName;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class ProjectFormalizeNameProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ProjectFormalizeNameModel, ProjectFormalizeName>().ReverseMap();
        }
    }
}