using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.TestCategory;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class TestCategoryModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<TestCategoryModel, TestCategory>().ReverseMap();
        }
    }
}