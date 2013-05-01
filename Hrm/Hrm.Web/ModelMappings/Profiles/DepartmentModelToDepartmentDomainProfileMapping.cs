using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Web.Models.Department;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class DepartmentModelToDepartmentDomainProfileMapping : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<DepartmentModel, Department>().ReverseMap();
        }
    }
}