using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.Selection;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class SelectedCandidateModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, SelectedCandidateModel>().ReverseMap();
        }
    }
}