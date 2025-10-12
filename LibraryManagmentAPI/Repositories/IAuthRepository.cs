using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<Author> Register(AddAuthorDto addAuthorDto);
        Task<string> Login(LogAuthorDto logAuthorDto);
    }
}
