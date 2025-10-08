using AutoMapper;
using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;

namespace LibraryManagmentAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Author, AddAuthorDto>().ReverseMap();
        }
    }
}
