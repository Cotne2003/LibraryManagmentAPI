using LibraryManagmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<BookDto>> GetAllBooks();
        Task<IEnumerable<BookDto>> GetAllAvailableBooks();
        Task<IEnumerable<BookDto>> GetUnavailableBooks();
        Task<BookDto> AddBook([FromBody] AddBookDto addBookDto);
        Task DeleteBook(Guid id);
    }
}
