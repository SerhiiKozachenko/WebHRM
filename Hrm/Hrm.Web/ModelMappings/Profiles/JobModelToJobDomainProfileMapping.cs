using AutoMapper;
using Hrm.Core.Entities;
using Hrm.Core.Interfaces.Repositories.Base;
using Hrm.Data.Implementations.Specifications.Common;
using Hrm.Web.Models.Job;
using Microsoft.Practices.ServiceLocation;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class JobModelToJobDomainProfileMapping : Profile
    {
        private IRepository<Department> depRepo;

        private IRepository<SkillMatrix> skillRepo;

        protected override void Configure()
        {
            Mapper.CreateMap<Job, JobModel>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id))
                .ForMember(dest => dest.SkillMatrixId, opt => opt.MapFrom(src => src.RequiredSkillMatrix.Id));

            Mapper.CreateMap<JobModel, Job>()
                .BeforeMap(InitRepos)
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => this.depRepo.FindOne(new ByIdSpecify<Department>(src.DepartmentId))))
                .ForMember(dest => dest.RequiredSkillMatrix, opt => opt.MapFrom(src => src.SkillMatrixId == null ? null : this.skillRepo.FindOne(new ByIdSpecify<SkillMatrix>(src.SkillMatrixId.Value))));
        }

        private void InitRepos(JobModel jobModel, Job job)
        {
            this.depRepo = ServiceLocator.Current.GetInstance<IRepository<Department>>();
            this.skillRepo = ServiceLocator.Current.GetInstance<IRepository<SkillMatrix>>();
        }
    }
}