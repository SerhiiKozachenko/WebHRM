﻿using AutoMapper;
using Hrm.Web.ModelMappings.Profiles;

namespace Hrm.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappingProfiles()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DepartmentModelToDepartmentDomainProfileMapping>();
                x.AddProfile<JobModelToJobDomainProfileMapping>();
                x.AddProfile<JobApplicationModelToJobApplicationDomainProfileMapping>();
                x.AddProfile<MySkillModelToUserSkillsDomainMappingProfile>();
                x.AddProfile<SkillCategoryModelToSkillCategoryDomainMappingProfile>();
                x.AddProfile<SkillModelToSkillDomainMappingProfile>();
                x.AddProfile<ProjectFormalizeNameProfile>();
                x.AddProfile<ProjectModelProfile>();
            });
        }
    }
}