using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Entities.Skills;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Web.Models.SkillMatrix;
using Hrm.Web.Models.SkillMatrix.SkillsModels;
using Microsoft.Practices.ServiceLocation;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class SkillMatrixModelToSkillMatrixDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<LanguageSkillModel, LanguageSkill>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            Mapper.CreateMap<ManagementSkillModel, ManagementSkill>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            Mapper.CreateMap<ProgrammingSkillModel, ProgrammingSkill>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            Mapper.CreateMap<DesignSkillModel, DesignSkill>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            Mapper.CreateMap<QualityAssuranceSkillModel, QualityAssuranceSkill>().ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();
            Mapper.CreateMap<SkillMatrixModel, SkillMatrix>()
                .BeforeMap(DeleteOrphans)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LanguageSkills, opt => opt.MapFrom(e => e.HasLanguageSkills ? e.LanguageSkills ?? new LanguageSkillModel() : null))
                .ForMember(dest => dest.ManagementSkills, opt => opt.MapFrom(e => e.HasManagementSkills ? e.ManagementSkills ?? new ManagementSkillModel() : null))
                .ForMember(dest => dest.ProgrammingSkills, opt => opt.MapFrom(e => e.HasProgrammingSkills ? e.ProgrammingSkills ?? new ProgrammingSkillModel() : null))
                .ForMember(dest => dest.DesignSkills, opt => opt.MapFrom(e => e.HasDesignSkills ? e.DesignSkills ?? new DesignSkillModel() : null))
                .ForMember(dest => dest.QualityAssuranceSkills, opt => opt.MapFrom(e => e.HasQualityAssuranceSkills ? e.QualityAssuranceSkills ?? new QualityAssuranceSkillModel() : null));
                
            Mapper.CreateMap<SkillMatrix, SkillMatrixModel>()
                .ForMember(dest => dest.HasLanguageSkills, opt => opt.MapFrom(e => e.LanguageSkills != null))
                .ForMember(dest => dest.HasManagementSkills, opt => opt.MapFrom(e => e.ManagementSkills != null))
                .ForMember(dest => dest.HasProgrammingSkills, opt => opt.MapFrom(e => e.ProgrammingSkills != null))
                .ForMember(dest => dest.HasDesignSkills, opt => opt.MapFrom(e => e.DesignSkills != null))
                .ForMember(dest => dest.HasQualityAssuranceSkills, opt => opt.MapFrom(e => e.QualityAssuranceSkills != null));
        }

        private void DeleteOrphans(SkillMatrixModel skillMatrixModel, SkillMatrix skillMatrix)
        {
            var langSkillRepo = ServiceLocator.Current.GetInstance<IRepository<LanguageSkill>>();
            var manSkillRepo = ServiceLocator.Current.GetInstance<IRepository<ManagementSkill>>();
            var progSkillRepo = ServiceLocator.Current.GetInstance<IRepository<ProgrammingSkill>>();
            var desSkillRepo = ServiceLocator.Current.GetInstance<IRepository<DesignSkill>>();
            var qaSkillRepo = ServiceLocator.Current.GetInstance<IRepository<QualityAssuranceSkill>>();

            if (!skillMatrixModel.HasLanguageSkills && skillMatrix.LanguageSkills != null)
            {
                langSkillRepo.Delete(skillMatrix.LanguageSkills);
            }

            if (!skillMatrixModel.HasManagementSkills && skillMatrix.ManagementSkills != null)
            {
                manSkillRepo.Delete(skillMatrix.ManagementSkills);
            }

            if (!skillMatrixModel.HasProgrammingSkills && skillMatrix.ProgrammingSkills != null)
            {
                progSkillRepo.Delete(skillMatrix.ProgrammingSkills);
            }

            if (!skillMatrixModel.HasDesignSkills && skillMatrix.DesignSkills != null)
            {
                desSkillRepo.Delete(skillMatrix.DesignSkills);
            }

            if (!skillMatrixModel.HasQualityAssuranceSkills && skillMatrix.QualityAssuranceSkills != null)
            {
                qaSkillRepo.Delete(skillMatrix.QualityAssuranceSkills);
            }
        }
    }
}