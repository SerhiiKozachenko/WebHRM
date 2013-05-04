using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.Test;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class TestModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<TestModel, Test>().ReverseMap();
        }
    }
}