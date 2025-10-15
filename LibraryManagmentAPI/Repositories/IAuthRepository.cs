using LibraryManagmentAPI.Models;
using LibraryManagmentAPI.Models.Entities;

namespace LibraryManagmentAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<Author> Register(AddAuthorDto addAuthorDto);
        Task<TokenResponseDto> Login(LogAuthorDto logAuthorDto);
        Task<TokenResponseDto> RefreshTokens(RefreshTokenRequestDto refreshTokenRequestDto);
    }
}
