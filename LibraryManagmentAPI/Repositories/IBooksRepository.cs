using LibraryManagmentAPI.Helpers;
using LibraryManagmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentAPI.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<BookDto>> GetAllBooks([FromQuery] QueryObject query);
        Task<BookDto> AddBook([FromBody] AddBookDto addBookDto);
        Task DeleteBook(Guid id);
    }
}
