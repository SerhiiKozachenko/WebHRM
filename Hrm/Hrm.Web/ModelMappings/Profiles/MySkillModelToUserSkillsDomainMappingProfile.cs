using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.MySkills;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class MySkillModelToUserSkillsDomainMappingProfile : Profile
    {
         protected override void Configure()
         {
             Mapper.CreateMap<UserSkill, MySkillModel>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.Skill.Name))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(e => e.Skill.Description))
                 .ForMember(dest => dest.Estimate, opt => opt.MapFrom(e => e.Estimate));
         }
    }
}