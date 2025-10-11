using LibraryManagmentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Repositories
{
    public interface IBorrowsRepository
    {
        Task<IEnumerable<BorrowDto>> GetAllBorrows();
        Task<BorrowDto> AddBorrow([FromBody] AddBorrowDto addBorrowDto);
        Task DeleteBorrow(Guid id);
    }
}
