using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.Skill;
using Microsoft.Practices.ServiceLocation;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class SkillModelToSkillDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SkillModel, Skill>()
                .AfterMap(MapSkillCategory);
            Mapper.CreateMap<Skill, SkillModel>()
                .ForMember(dest => dest.SkillCategoryId, opt => opt.MapFrom(e => e.SkillCategory.Id));
        }

        private void MapSkillCategory(SkillModel skillModel, Skill skill)
        {
            //var skillCatRepo = ServiceLocator.Current.GetInstance<IRepository<SkillCategory>>();
            //var skillCat = skillCatRepo.FindOne(new ByIdSpecify<SkillCategory>(skillModel.SkillCategoryId));
            //skill.SkillCategory = skillCat;
        }
    }
}