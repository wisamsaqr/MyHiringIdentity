using AutoMapper;
using Hiring.Data.Models;
using Hiring.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Hiring.Service.Utilities
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserVm>();          // GetAll
            CreateMap<CreateUserVm, User>();    // Create
            CreateMap<User, EditUserVm>();      // GetEditVm
            CreateMap<EditUserVm, User>();      // Edit

            CreateMap<Admin, AdminVm>();
            CreateMap<CreateAdminVm, Admin>();
            CreateMap<Admin, EditAdminVm>();
            CreateMap<EditAdminVm, Admin>();
            
            CreateMap<Foundation, FoundationVm>();
            CreateMap<CreateFoundationVm, Foundation>();
            CreateMap<Foundation, EditFoundationVm>();
            CreateMap<EditFoundationVm, Foundation>();
            ////
            //CreateMap<Foundation, FoundationVm2>();
            //CreateMap<CreateFoundationVm2, Foundation>();
            //CreateMap<Foundation, EditFoundationVm2>();
            //CreateMap<EditFoundationVm2, Foundation>();

            CreateMap<JobSeeker, JobSeekerVm>();
            CreateMap<CreateJobSeekerVm, JobSeeker>();
            CreateMap<JobSeeker, EditJobSeekerVm>();
            CreateMap<EditJobSeekerVm, JobSeeker>();

            CreateMap<JobAdvertisement, JobAdvertisementVm>();
            CreateMap<JobAdvertisement, JobAdvertisementDetailsVm>();
            CreateMap<CreateJobAdvertisementVm, JobAdvertisement>().ForMember(x => x.Attachments, x => x.Ignore());
            CreateMap<JobAdvertisement, EditJobAdvertisementVm>();
            CreateMap<EditJobAdvertisementVm, JobAdvertisement>();
        }
    }
}