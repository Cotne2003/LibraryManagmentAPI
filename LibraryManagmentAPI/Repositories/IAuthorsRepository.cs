using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Repositories
{
    public interface IAuthorsRepository
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthors();
        Task<Author> AddAuthor([FromBody] AddAuthorDto addAuthorDto);
        Task DeleteAuthor(Guid id);
    }
}
