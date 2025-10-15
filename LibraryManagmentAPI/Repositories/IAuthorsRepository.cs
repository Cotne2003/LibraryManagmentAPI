using LibraryManagmentAPI.Models;

namespace LibraryManagmentAPI.Repositories
{
    public interface IAuthorsRepository
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthors();
        Task DeleteAuthor(Guid id);
    }
}
