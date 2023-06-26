using AutoMapper;
using StudentAdminPortalAPI.DomainModels;
using DataModels = StudentAdminPortalAPI.Models;

namespace StudentAdminPortalAPI.Profiles
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<DataModels.Student, Student>().ReverseMap();

            CreateMap<DataModels.Gender, Gender>().ReverseMap();

            CreateMap<DataModels.Address, Address>().ReverseMap();
        }
    }
}
