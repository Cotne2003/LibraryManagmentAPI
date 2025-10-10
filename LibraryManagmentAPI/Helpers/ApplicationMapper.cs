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
            CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books.Select(b => b.Id).ToList()));
            CreateMap<Member, AddMemberDto>().ReverseMap();
            CreateMap<Book, AddBookDto>().ReverseMap();
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(a => a.Id).ToList()));
        }
    }
}
