using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Data.EF.Repositories.Contracts;
using Hrm.Data.EF.Specifications.Implementations.Common;
using Hrm.Web.Models.Job;
using Microsoft.Practices.ServiceLocation;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class JobModelToJobDomainProfileMapping : Profile
    {
        private IRepository<Department> depRepo;

        protected override void Configure()
        {
            Mapper.CreateMap<Job, JobModel>()
                  .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.Id));

            Mapper.CreateMap<JobModel, Job>();
            //.BeforeMap(InitRepos)
            //.ForMember(dest => dest.DepartmentId,
            //           opt =>
            //           opt.MapFrom(src => this.depRepo.FindOne(new ByIdSpecify<Department>(src.DepartmentId))));
        }

        //private void InitRepos(JobModel jobModel, Job job)
        //{
        //    this.depRepo = ServiceLocator.Current.GetInstance<IRepository<Department>>();
        //}
    }
}