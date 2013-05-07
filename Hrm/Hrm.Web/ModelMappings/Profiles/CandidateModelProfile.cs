using System;
using AutoMapper;
using Hrm.Data.EF.Models;
using Hrm.Web.Models.SelectionResult;
using Profile = AutoMapper.Profile;

namespace Hrm.Web.ModelMappings.Profiles
{
    public class CandidateModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, CandidateModel>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src =>
                    (src.Profile != null) ? src.Profile.PhoneNumber : "Empty"))
                .ForMember(dest => dest.Skype, opt => opt.MapFrom(src =>
                    (src.Profile != null) ? src.Profile.Skype : "Empty"))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
                    (src.Profile != null && src.Profile.DateOfBirth.HasValue) ? src.Profile.DateOfBirth.Value.ToShortDateString() : "Empty"))
                .ForMember(dest => dest.LastJobTitle, opt => opt.MapFrom(src =>
                    (src.Profile != null) ? src.Profile.LastJobTitle : "Empty"))
                .ForMember(dest => dest.TotalWorkExperience, opt => opt.MapFrom(src =>
                    (src.Profile != null && src.Profile.TotalWorkExperience.HasValue) ? src.Profile.TotalWorkExperience.Value.ToString() : "Empty"))
                .ForMember(dest => dest.ResumePath, opt => opt.MapFrom(src =>
                    (src.Profile != null) ? src.Profile.ResumePath : "Empty"));
                  

            Mapper.CreateMap<CandidateModel, User>();

        }

        private void AfterFunction(User user, CandidateModel candidateModel)
        {
            
        }
    }
}